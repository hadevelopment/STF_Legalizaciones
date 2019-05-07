using DinkToPdf;
using DinkToPdf.Contracts;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Interface;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Legalizaciones.Interface.ISolicitud;

namespace Legalizaciones.Web.Controllers
{
    public class ExportFilesFormatController : ControllerBase
    {
        private IConverter _converter;
        private readonly ISolicitudRepository solicitudRepository;
        private readonly IEstadoSolicitudRepository estatusRepository;

        public ExportFilesFormatController(IConverter converter, 
                                           ISolicitudRepository solicitudRepository,
                                           IEstadoSolicitudRepository estatusRepository)
        {
            _converter = converter;
            this.solicitudRepository = solicitudRepository;
            this.estatusRepository   = estatusRepository;
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
        public ActionResult ExportDatosSolicitudExcel()
        {
            List<Solicitud> lstSolicitudes = this.solicitudRepository.All().ToList();
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
                row.CreateCell(5).SetCellValue("Monto Beneficiario");
                row.CreateCell(6).SetCellValue("Estado");

                var index = 1;
                foreach (var solicitud in lstSolicitudes)
                {
                    row = excelSheet.CreateRow(index);
                    row.CreateCell(0).SetCellValue(solicitud.Id);
                    row.CreateCell(1).SetCellValue(solicitud.FechaSolicitud.ToShortDateString());
                    row.CreateCell(2).SetCellValue(solicitud.FechaVencimiento.ToShortDateString());
                    row.CreateCell(3).SetCellValue(solicitud.Concepto);
                    row.CreateCell(4).SetCellValue(solicitud.EmpleadoCedula);
                    row.CreateCell(5).SetCellValue(solicitud.Monto);
                    row.CreateCell(6).SetCellValue(estatusRepository.Find(long.Parse(solicitud.EstadoID.ToString())).Descripcion);
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
