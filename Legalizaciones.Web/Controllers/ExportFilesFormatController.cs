using DinkToPdf;
using DinkToPdf.Contracts;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Interface;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Legalizaciones.Interface.IJerarquia;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model.Jerarquia;
using Legalizaciones.Web.Helpers;

namespace Legalizaciones.Web.Controllers
{
    public class ExportFilesFormatController : ControllerBase
    {
        private IConverter _converter;
        private readonly ISolicitudRepository solicitudRepository;
        private readonly IBancoRepository bancoRepository;
        private readonly ILegalizacionRepository legalizacionRepository;
        private readonly IEmpleadoRepository empleadoRepository;
        private readonly IZonaRepository zonaRepository;
        private readonly IDestinoRepository destinoRepository;
        private readonly IEstadoSolicitudRepository estatusRepository;
        private readonly IOrigenDestinoRepository origenDestinoRepository;
        private readonly IPaisRepository paisRepository;
        private readonly IConfiguracionGastoRepository ConfiguracionGastoRepository;


        public ExportFilesFormatController(IConverter converter, 
                                           ISolicitudRepository solicitudRepository,
                                           IZonaRepository    zonaRepository,
                                           IDestinoRepository destinoRepository,
                                           IEstadoSolicitudRepository estatusRepository,
                                           IOrigenDestinoRepository origenDestinoRepository,
                                           IPaisRepository paisRepository,
                                           IEmpleadoRepository empleadoRepository,
                                           ILegalizacionRepository legalizacionRepository,
                                           IBancoRepository bancoRepository,
                                           IConfiguracionGastoRepository ConfiguracionGastoRepository)
        {
            _converter = converter;
            this.solicitudRepository = solicitudRepository;
            this.estatusRepository   = estatusRepository;
            this.zonaRepository      = zonaRepository;
            this.destinoRepository       = destinoRepository;
            this.origenDestinoRepository = origenDestinoRepository;
            this.paisRepository = paisRepository;
            this.empleadoRepository = empleadoRepository;
            this.legalizacionRepository = legalizacionRepository;
            this.bancoRepository = bancoRepository;
            this.ConfiguracionGastoRepository = ConfiguracionGastoRepository;
        }

        [HttpGet]
        public ActionResult CreatePDF(int id)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\solicitud.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                Page = $"http://{Request.Host}/Solicitud/VisorPDF?id=" + id.ToString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return Download("Solicitud.pdf");

        }


        [HttpGet]
        public ActionResult CreateLegalizacionesPDF(int id)
        {
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = "PDF Report",
                Out = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\Legalizaciones.pdf"
            };

            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                Page = $"http://{Request.Host}/Legalizaciones/VisorLegalizacionPDF?id=" + id.ToString(),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css") },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
            };

            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };

            var file = _converter.Convert(pdf);

            return Download("Legalizaciones.pdf");

        }

        [HttpGet]
        public ActionResult ExportDatosSolicitudExcel()
        {
            UNOEE erp = new UNOEE();
            List<Solicitud> lstSolicitudes = this.solicitudRepository.All().ToList();
            foreach (var item in lstSolicitudes)
            {
                item.Empleado = erp.getEmpleadoCedula(item.EmpleadoCedula);
            }

            //foreach (var item in lstSolicitudes)
            //{
            //    item.Empleado = empleadoRepository.All().FirstOrDefault(x => x.Cedula == item.EmpleadoCedula);
            //}

            string sFileName = "Solicitudes.xls";
            string sWebRootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Fecha Solicitud");
                row.CreateCell(2).SetCellValue("Fecha Vencimiento");
                row.CreateCell(3).SetCellValue("Concepto Anticipio");
                row.CreateCell(4).SetCellValue("Cedula del Beneficiario");
                row.CreateCell(5).SetCellValue("Nombre del Beneficiario");
                row.CreateCell(6).SetCellValue("Monto Beneficiario");
                row.CreateCell(7).SetCellValue("Estado");

                var index = 1;
                foreach (var solicitud in lstSolicitudes)
                {
                    row = excelSheet.CreateRow(index);
                    row.CreateCell(0).SetCellValue(solicitud.Id);
                    row.CreateCell(1).SetCellValue(solicitud.FechaSolicitud.ToShortDateString());
                    row.CreateCell(2).SetCellValue(solicitud.FechaVencimiento.ToShortDateString());
                    row.CreateCell(3).SetCellValue(solicitud.Concepto);
                    row.CreateCell(4).SetCellValue(solicitud.EmpleadoCedula);
                    row.CreateCell(5).SetCellValue(solicitud.Empleado.Nombre + ' ' + solicitud.Empleado.Apellido);
                    row.CreateCell(6).SetCellValue(double.Parse(solicitud.Monto.ToString()));
                    row.CreateCell(7).SetCellValue(estatusRepository.Find(long.Parse(solicitud.EstadoId.ToString())).Descripcion);
                    index++;
                }

                workbook.Write(fs);
            }

            return Download(sFileName);
        }


        [HttpGet]
        public ActionResult ExportDatosLegalizacionesExcel()
        {
            UNOEE erp = new UNOEE();
            List<Legalizacion> lstLegalizaciones = this.legalizacionRepository.All().ToList();
            Solicitud solicitud;
            foreach (var item in lstLegalizaciones)
            {
                item.Banco = bancoRepository.All().FirstOrDefault(x => x.Id == item.BancoId);
            }

            string sFileName = "Legalizaciones.xls";
            string sWebRootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Legalizaciones");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Recibo Caja");
                row.CreateCell(2).SetCellValue("Consignacion");
                row.CreateCell(3).SetCellValue("Valor");
                row.CreateCell(4).SetCellValue("Banco");

                row.CreateCell(5).SetCellValue("Numero Solicitud");
                row.CreateCell(6).SetCellValue("Fecha Solicitud");
                row.CreateCell(7).SetCellValue("Fecha Vencimiento");
                row.CreateCell(8).SetCellValue("Concepto Anticipio");
                row.CreateCell(9).SetCellValue("Cedula del Beneficiario");
                row.CreateCell(10).SetCellValue("Nombre del Beneficiario");
                row.CreateCell(11).SetCellValue("Monto Beneficiario");
                row.CreateCell(12).SetCellValue("Estado");

                var index = 1;
                foreach (var legalizacion in lstLegalizaciones)
                {
                    row = excelSheet.CreateRow(index);

                    row.CreateCell(0).SetCellValue(legalizacion.Id);
                    row.CreateCell(1).SetCellValue(legalizacion.ReciboCaja);
                    row.CreateCell(2).SetCellValue(legalizacion.Consignacion);
                    row.CreateCell(3).SetCellValue(legalizacion.Valor);
                    row.CreateCell(4).SetCellValue(legalizacion.Banco.Nombre);

                    solicitud = solicitudRepository.All().FirstOrDefault(x => x.Id == legalizacion.SolicitudID);
                    //solicitud.Empleado = empleadoRepository.All()
                    //    .FirstOrDefault(x => x.Cedula == solicitud.EmpleadoCedula);

                    solicitud.Empleado = erp.getEmpleadoCedula(solicitud.EmpleadoCedula);

                    row.CreateCell(5).SetCellValue(solicitud.Id);
                    row.CreateCell(6).SetCellValue(solicitud.FechaSolicitud.ToShortDateString());
                    row.CreateCell(7).SetCellValue(solicitud.FechaVencimiento.ToShortDateString());
                    row.CreateCell(8).SetCellValue(solicitud.Concepto);
                    row.CreateCell(9).SetCellValue(solicitud.EmpleadoCedula);
                    row.CreateCell(10).SetCellValue(solicitud.Empleado.Nombre + ' ' + solicitud.Empleado.Apellido);
                    row.CreateCell(11).SetCellValue(double.Parse(solicitud.Monto.ToString()));
                    row.CreateCell(12).SetCellValue(estatusRepository.Find(long.Parse(solicitud.EstadoId.ToString())).Descripcion);
                    index++;
                }

                workbook.Write(fs);
            }

            return Download(sFileName);
        }

        [HttpGet]
        public ActionResult ExportConfiguracionGastosExcel()
        {
            List<ConfiguracionGasto> lstConfGastos = this.ConfiguracionGastoRepository.All().Where(a => a.Estatus == 1).ToList();
            foreach (var item in lstConfGastos)
            {
                item.Pais = paisRepository.Find(long.Parse(item.PaisId.ToString()));
            }

            string sFileName = "ConfiguracionGastos.xls";
            string sWebRootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("ConfiguracionGastos");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Descripcion");
                row.CreateCell(2).SetCellValue("Cargo");
                row.CreateCell(3).SetCellValue("Pais");
                row.CreateCell(4).SetCellValue("Tipo Servicio");
                row.CreateCell(5).SetCellValue("Origen");
                row.CreateCell(6).SetCellValue("Destino");
                row.CreateCell(7).SetCellValue("Moneda");
                row.CreateCell(8).SetCellValue("Gasto Diario");
                row.CreateCell(9).SetCellValue("Monto");
                

                var index = 1;
                foreach (var itemOrigenDestino in lstConfGastos)
                {
                    row = excelSheet.CreateRow(index);
                    row.CreateCell(0).SetCellValue(itemOrigenDestino.Id);
                    row.CreateCell(1).SetCellValue(itemOrigenDestino.Descripcion);
                    row.CreateCell(2).SetCellValue(itemOrigenDestino.Cargo);
                    row.CreateCell(3).SetCellValue(itemOrigenDestino.Pais.Nombre);
                    row.CreateCell(4).SetCellValue(itemOrigenDestino.TipoServicio);
                    row.CreateCell(5).SetCellValue(itemOrigenDestino.OrigenNombre);
                    row.CreateCell(6).SetCellValue(itemOrigenDestino.DestinoNombre);
                    row.CreateCell(7).SetCellValue(itemOrigenDestino.MonedaNombre);
                    row.CreateCell(8).SetCellValue(itemOrigenDestino.GastoDiario);
                    row.CreateCell(9).SetCellValue(itemOrigenDestino.Monto);
         
                    index++;
                }

                workbook.Write(fs);
            }

            return Download(sFileName);
        }

        [HttpGet]
        public ActionResult ExportDatosOrigenDestinoExcel()
        {
            List<OrigenDestino> lstOrigenDestino = this.origenDestinoRepository.All().Where(a => a.Estatus == 1).ToList();
            foreach (var item in lstOrigenDestino)
            {
                item.Pais = paisRepository.Find(long.Parse(item.PaisId.ToString()));
            }

            string sFileName = "OrigenDestino.xls";
            string sWebRootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("OrigenDestino");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Nombre");
                row.CreateCell(2).SetCellValue("Pais");
                row.CreateCell(3).SetCellValue("Fecha Creacion");

                var index = 1;
                foreach (var itemOrigenDestino in lstOrigenDestino)
                {
                    row = excelSheet.CreateRow(index);
                    row.CreateCell(0).SetCellValue(itemOrigenDestino.Id);
                    row.CreateCell(1).SetCellValue(itemOrigenDestino.Nombre);
                    row.CreateCell(2).SetCellValue(itemOrigenDestino.Pais.Nombre);
                    row.CreateCell(3).SetCellValue(itemOrigenDestino.FechaCreacion.ToShortDateString());
                    index++;
                }

                workbook.Write(fs);
            }

            return Download(sFileName);
        }


        [HttpGet]
        public ActionResult ExportDatosZonasExcel()
        {
            List<Zona> lstZonas = this.zonaRepository.All().Where(a => a.Estatus == 1).ToList();
            foreach (var zona in lstZonas)
            {
                zona.Destino = destinoRepository.Find(long.Parse(zona.DestinoID.ToString()));
            }

            string sFileName = "Zonas.xls";
            string sWebRootFolder = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            var memory = new MemoryStream();
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Zona");
                IRow row = excelSheet.CreateRow(0);

                row.CreateCell(0).SetCellValue("ID");
                row.CreateCell(1).SetCellValue("Nombre");
                row.CreateCell(2).SetCellValue("Abreviatura");
                row.CreateCell(3).SetCellValue("Destino");
                row.CreateCell(4).SetCellValue("Fecha Creacion");

                var index = 1;
                foreach (var zona in lstZonas)
                {
                    row = excelSheet.CreateRow(index);
                    row.CreateCell(0).SetCellValue(zona.Id);
                    row.CreateCell(1).SetCellValue(zona.Nombre);
                    row.CreateCell(2).SetCellValue(zona.Abreviatura);
                    row.CreateCell(3).SetCellValue(zona.Destino.Nombre);
                    row.CreateCell(4).SetCellValue(zona.FechaCreacion.ToShortDateString());
                    index++;
                }

                workbook.Write(fs);
            }

            return Download(sFileName);
        }

        public ActionResult Download(string filename)
        {
            if (filename == null)
                return Content("filename not present");

            var path = Directory.GetCurrentDirectory() + "\\wwwroot\\files\\" + filename;

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            memory.Position = 0;
            return File(memory, GetContentType(path), Path.GetFileName(path));
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }
    }
}
