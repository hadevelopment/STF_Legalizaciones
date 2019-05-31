using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Legalizaciones.Interface;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using Legalizaciones.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Legalizaciones.Web.Models.ViewModel;
using Legalizaciones.Web.Engine;
using Legalizaciones.Model.Workflow;

namespace Legalizaciones.Web.Controllers
{
    [Route("Solicitud")]
    public class SolicitudController : Controller
    {
        private readonly ISolicitudRepository solicitudRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private readonly ITipoSolicitudRepository tipoSolicitudRepository;
        private readonly IHistoricoSolicitudRepository historicoSolicitudRepository;
        private readonly IPasoFlujoSolicitudRepository pasoFlujoSolicitudRepository;
        public Solicitud sol;
        public List<SolicitudGastos> lstSolicitudGastos = new List<SolicitudGastos>();
        public UNOEE objUNOEE = new UNOEE();

        public readonly IEmpleadoRepository empleadoRepository;
        public readonly IMonedaRepository monedaRepository;
        public readonly IDestinoRepository destinoRepository;
        public readonly IZonaRepository zonaRepository;
        public readonly IEstadoSolicitudRepository estatusRepository;
        EngineDb DB = new EngineDb();

        public SolicitudController(
            ISolicitudRepository solicitudRepository,
            ISolicitudGastosRepository solicitudGastosRepository,
            ITipoSolicitudRepository tipoSolicitudRepository,
            IHistoricoSolicitudRepository historicoSolicitudRepository,
            IMonedaRepository monedaRepository,
            IDestinoRepository destinoRepository,
            IZonaRepository zonaRepository,
            IEstadoSolicitudRepository estatusRepository,
            IPasoFlujoSolicitudRepository pasoFlujoSolicitudRepository
            )
        {
            this.solicitudRepository = solicitudRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.tipoSolicitudRepository = tipoSolicitudRepository;
            this.historicoSolicitudRepository = historicoSolicitudRepository;
            this.monedaRepository = monedaRepository;
            this.destinoRepository = destinoRepository;
            this.zonaRepository = zonaRepository;
            this.estatusRepository = estatusRepository;
            this.pasoFlujoSolicitudRepository = pasoFlujoSolicitudRepository;
        }

        public IActionResult Index()
        {
            UNOEE erp = new UNOEE();
            var cedula = "";
            var cargo = "";
            //List<SolicitudAnticipioView> solicitudes = new List <SolicitudAnticipioView>();
            List<Solicitud> solicitudes = new List<Solicitud>();

            /*
             * HAY 3 empleados (del 1 al 3)
             * EMPLEADO = 1 Tiene ROL EMPLEADO ROL = 1
             * EMPLEADO = 2 Tiene ROL Adm. Tesoreria ROL = 2
             * EMPLEADO = 3 Tiene ROL Adm. Contraloria ROL = 3
            */

            // Obtenemos datos del empleado en Session
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cedula")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cedula = HttpContext.Session.GetString("Usuario_Cedula");
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            ViewBag.cargo = cargo;

            //Si es administrador puede ver todas las solicitudes
            if (cargo == "3")
            {
                 solicitudes = solicitudRepository.All().ToList();
                //var q = from sol in dbContext.Solicitud
                //        join es in dbContext.EstadoSolicitud on sol.Estatus  equals es.Id 
                //        join emp in dbContext.Empleado on sol.EmpleadoCedula equals emp.Cedula
                //    select new { Id = sol.Id,
                //                 NumeroSolicitud = sol.NumeroSolicitud,
                //                 FechaSolicitud  = sol.FechaSolicitud,
                //                 Concepto        = sol.Concepto,
                //                 CedulaBenefi    = sol.EmpleadoCedula,
                //                 Beneficiario    = emp.Nombre + ' ' + emp.Apellido,
                //                 Monto           = sol.Monto.ToString(),
                //                 EstadoSoli      = es.Descripcion,
                //                 FechaPago       = sol.FechaVencimiento
                //    };



                foreach (var item in solicitudes)
                {
                    item.Empleado = erp.getEmpleadoCedula(item.EmpleadoCedula);
                    item.EstadoSolicitud = estatusRepository.Find(long.Parse(item.EstadoId.ToString()));
                }

                return View(solicitudes);
            }
            //Si no es administrador solo puede ver las solicitudes creadas por el
            else
            {
                solicitudes = solicitudRepository.All().Where(e => e.EmpleadoCedula == cedula).ToList();

                foreach (var item in solicitudes)
                {
                    item.Empleado = erp.getEmpleadoCedula(item.EmpleadoCedula);
                    item.EstadoSolicitud = estatusRepository.All().Where(e => e.Id == item.EstadoId).ToList().FirstOrDefault();
                }

                return View(solicitudes);
            }
        }

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear()
        {
            var cedula = "";
            var cargo = "";
            Empleado empleado = null;

            // Obtenemos datos del empleado en Session
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cedula")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cedula = HttpContext.Session.GetString("Usuario_Cedula");
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            //Se obtiene el objeto empleado
            empleado = objUNOEE.getEmpleadoCedula(cedula);

            Solicitud solicitud = new Solicitud();
            solicitud.Empleado = empleado;

            return View(solicitud);
        }

        [HttpPost]
        [Route("Crear")]
        public ActionResult Guardar(Solicitud solicitud, IFormFile file)
        {

            try
            {
                solicitud.NumeroSolicitud = String.Format("{0:yyyMMddHHmmmss}", DateTime.Now);
                List<SolicitudGastos> listaGastos = new List<SolicitudGastos>();
                solicitud.EmpleadoCedula = solicitud.Empleado.Cedula;
                solicitud.FechaCreacion = DateTime.Now;
                solicitud.Estatus = 1;//Activa
                solicitud.EstadoId = 1;//Estado Sin Legalizar
                solicitud.TipoSolicitudID = 1;//Anticipo
                solicitud.FechaDesde = DateTime.Now;
                solicitud.FechaHasta = DateTime.Now;
                solicitud.FechaSolicitud = DateTime.Now;

                //Calculo Fecha de Vencimiento de la Solicitud
                var DiasHabiles = tipoSolicitudRepository.All().Where(a => a.Id == 1).FirstOrDefault().DiasHabiles;
                solicitud.FechaVencimiento = solicitud.FechaHasta.AddDays(DiasHabiles);

                    //Se obtiene el paso inicial del flujo configurado
                    solicitud.PasoFlujoSolicitudId = getPasoInicialFlujo();

                    //Se Registra la Solicitud
                    solicitudRepository.Insert(solicitud);

                listaGastos = JsonConvert.DeserializeObject<List<SolicitudGastos>>(solicitud.GastosJSON.Replace("Fecha Gasto", "FechaGasto"));
                solicitud.SolicitudGastos = listaGastos;

                //Se Registran los gastos de la Solicitud
                foreach (SolicitudGastos item in listaGastos)
                {
                    item.SolicitudId = solicitud.Id;
                    solicitudGastosRepository.Insert(item);
                }

                //Se guarda la carta de descuento en el directorio en caso de que exista
                if (solicitud.Carta != null)
                {
                    if (solicitud.Carta.FileName != "")
                    {
                        //Se sube al directorio
                        SubirArchivo(solicitud.Carta, "files\\carta\\", solicitud.Empleado.Cedula, solicitud.Id.ToString());

                        //Se actualiza la ruta en BD
                        solicitud.RutaArchivo = getRuta(solicitud.Carta, "files/carta/", solicitud.Empleado.Cedula, solicitud.Id.ToString());
                        solicitudRepository.Update(solicitud);
                    }
                }

                TempData["Alerta"] = "success - La Solicitud se registro correctamente.";
                return RedirectToAction("Index", "Solicitud");
            }
            catch (System.Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la solicitud";

                return View("Crear", solicitud);
            }



        }

        [HttpGet]
        [Route("Editar")]
        public ActionResult Editar(int id)
        {
            try
            {
                var OSolicitud = solicitudRepository.Find(id);
                if (OSolicitud.TipoSolicitudID == 1)
                {
                    var empleado = objUNOEE.getEmpleadoCedula(OSolicitud.EmpleadoCedula);

                    OSolicitud.Empleado = empleado;

                    var ListaSolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == id).ToList();
                    OSolicitud.SolicitudGastos = ListaSolicitudGastos;

                    return View(OSolicitud);

                }
                else
                {
                    return RedirectToAction("EditarSolicitudTDC", routeValues: new { id = id});
                }
            }
            catch (Exception e)
            {
                TempData["Alerta"] = "error - No se pudo Encontrar la Solicitud";
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        [Route("Editar")]
        public ActionResult Editar(Solicitud data)
        {
            try
            {
                //if (!ModelState.IsValid || data.Id == 0)
                //    return View(data);

                List<SolicitudGastos> listaGastos = new List<SolicitudGastos>();
                listaGastos = JsonConvert.DeserializeObject<List<SolicitudGastos>>(data.GastosJSON.Replace("Fecha Gasto", "FechaGasto"));
                data.Monto = listaGastos.Sum(a => a.Monto);
                solicitudRepository.Update(data);

                //Elimino la tabla de detalles
                var LisGastosRegistrados = solicitudGastosRepository.All().Where(a => a.SolicitudId == data.Id).ToList();
                foreach (var item in LisGastosRegistrados)
                {
                    solicitudGastosRepository.Delete(item);
                }
                //Ahora voy a agregar el detalle de las solicitudes
                foreach (var item in listaGastos)
                {
                    item.SolicitudId = data.Id;
                    solicitudGastosRepository.Insert(item);
                }

                TempData["Alerta"] = "success - La Solicitud se Actualizo correctamente.";
                return RedirectToAction("Index", "Solicitud");

            }
            catch (Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de actualizar la solicitud.";
                return RedirectToAction("Editar", "Solicitud", data.Id);
            }

        }


        /*     ****************************************************************************
         *     Descripcion: VISTA para exortar datos al Excel
         *     Creada: 23-04-2019 
         *     Autor: Javier Rodriguez    
               **************************************************************************** */
        [HttpGet]
        [Route("Exportar")]
        public ActionResult ExportAtExcell()
        {
            var memory = new MemoryStream(); 
            string sFileName = "demo.xls";
            var util = new ImportExportModel();
            util.Scheme = Request.Scheme;
            util.Host   = Request.Host;
            memory = util.OnPostExport(sFileName);
            return File(memory, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", sFileName);

        }


        /*     ****************************************************************************
         *     Descripcion: VISTA para acceder a datos de la Solicitud. 
         *     Creada: 23-04-2019 
         *     Autor: Javier Rodriguez    
               **************************************************************************** */
        [HttpGet]
        [Route("VisorPDF")]
        public ActionResult VisorPDF(int id)
        {
            UNOEE erp = new UNOEE();
            var res = solicitudRepository.Find(id);

            res.CentroCosto = erp.getCentroCosto(res.CentroCostoId);
            res.CentroOperacion = erp.getCentroOperacion(res.CentroOperacionId);
            res.Moneda = Get_moneda(res.MonedaId);
            res.UnidadNegocio = erp.getUnidadNegocio(res.UnidadNegocioId);
            res.Empleado = erp.getEmpleadoCedula(res.EmpleadoCedula);
            res.Destino = Get_Destino(res.DestinoID);
            res.Zona = Get_Zona(res.ZonaID);

            List<SolicitudGastos> Lista = new List<SolicitudGastos>();
            Lista = solicitudGastosRepository.All().Where(x => x.SolicitudId == id).ToList();

            res.SolicitudGastos = Lista;
            return View(res);
        }

        /*     ****************************************************************************
         *     Descripcion: VISTA para acceder a datos de la Solicitud. 
         *     Creada: 23-04-2019 
         *     Autor: Joan Marchant    
               **************************************************************************** */
        [HttpGet]
        [Route("Visualizar")]
        public ActionResult Visualizar(int id)
        {
            UNOEE erp = new UNOEE();
            var res = solicitudRepository.Find(id);

            res.CentroCosto = erp.getCentroCosto(res.CentroCostoId); 
            res.CentroOperacion = erp.getCentroOperacion(res.CentroOperacionId);
            res.Moneda = Get_moneda(res.MonedaId);
            res.UnidadNegocio = erp.getUnidadNegocio(res.UnidadNegocioId);
            res.Empleado = erp.getEmpleadoCedula(res.EmpleadoCedula);
            res.Destino = Get_Destino(res.DestinoID);
            res.Zona = Get_Zona(res.ZonaID);

            List<SolicitudGastos> Lista = new List<SolicitudGastos>();
            Lista = solicitudGastosRepository.All().Where(x => x.SolicitudId == id).ToList();

            res.SolicitudGastos = Lista;

            List<Flujo> lstFlujo = new List<Flujo>();
            lstFlujo = DB.ObtenerFlujoSolicitud(id);

            if (lstFlujo.Count > 0)
                res.ListaFlujo = lstFlujo.OrderBy(m => m.Orden).ToList();

            return View(res);
        }




        [HttpGet]
        [Route("RegistrarSTDC")]
        public ActionResult RegistrarSTDC()
        {
            var wSolcitudTDC = new SolicitudTDCViewModel();
            return View(wSolcitudTDC);
        }

        [HttpPost]
        [Route("RegistrarSTDC")]
        public ActionResult RegistrarSTDC(SolicitudTDCViewModel solicitudTDC)
        {
            try
            {
                var OSolicitud = new Solicitud
                {
                    NumeroSolicitud = String.Format("{0:yyyMMddHHmmmss}", DateTime.Now),
                    EmpleadoCedula = solicitudTDC.Cedula,
                    FechaCreacion = DateTime.Now,
                    Estatus = 1,
                    TipoSolicitudID = 2,
                    FechaVencimiento = DateTime.Now,
                    Concepto = "Solicitud de Anticipo Con TDC",
                    DestinoID = 1,
                    ZonaID = 1,
                    CentroOperacionId = 1,
                    UnidadNegocioId = 1,
                    CentroCostoId = 1,
                    FechaDesde = DateTime.Now,
                    FechaHasta = DateTime.Now,
                    MonedaId = 1,
                    FechaSolicitud = DateTime.Now,
                    Banco = solicitudTDC.Banco,
                    Extracto = solicitudTDC.Extracto,
                    Monto = solicitudTDC.Monto,
                    EstadoId = 1,

                };

                solicitudRepository.Insert(OSolicitud);


                TempData["Alerta"] = "success - La Solicitud se registro correctamente.";
                return RedirectToAction("RegistrarSTDC", "Solicitud");
            }
            catch (System.Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la solicitud";
                return View(solicitudTDC);
            }

        }


        [HttpGet]
        [Route("EditarSolicitudTDC")]
        public ActionResult EditarSolicitudTDC(int id)
        {
            try
            {
                var OSolicitud = solicitudRepository.Find(id);
                var OsolicitudTDC = new SolicitudTDCViewModel
                {
                    Cedula = OSolicitud.EmpleadoCedula,
                    Banco = OSolicitud.Banco,
                    Extracto = OSolicitud.Extracto,
                    Monto = OSolicitud.Monto,
                    id = OSolicitud.Id
                };
                return View(OsolicitudTDC);

            }
            catch (Exception e)
            {
                TempData["Alerta"] = "error - No se pudo Encontrar la Solicitud de tajeta de Crédito";
                return RedirectToAction("Index", "Home");
            }

        }


        [HttpPost]
        [Route("EditarSolicitudTDC")]
        public ActionResult EditarSolicitudTDC(SolicitudTDCViewModel data)
        {
            try
            {
                var OSolicitud = solicitudRepository.Find(data.id);
                OSolicitud.EmpleadoCedula = data.Cedula;
                OSolicitud.Banco = data.Banco;
                OSolicitud.Monto = data.Monto;
                OSolicitud.Extracto = data.Extracto;
                solicitudRepository.Update(OSolicitud);

                TempData["Alerta"] = "success - La Solicitud de Tajeta de Crédito se Actualizo correctamente.";
                return RedirectToAction("Index", "Solicitud");

            }
            catch (Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de actualizar la solicitud de Tarjeta de Crédito.";
                return View(data);
            }

        }


        /* ****************************************************************************
*     Descripcion: VISTA para exortar datos al Excel
*     Creada: 09-05-2019 
*     Autor: Javier Rodriguez    
      **************************************************************************** */
        [HttpPost]
        [Route("Filtrar")]
        public ActionResult Filtrar(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<Solicitud> solicitudes = solicitudRepository.All()
                .Where(a => a.FechaCreacion >= fechaDesde && a.FechaCreacion <= fechaHasta).ToList();
            return View("Index", solicitudes);
        }


        #region Metodos sin acciones

        /*     ****************************************************************************
 *     Descripcion: METODO para acceder a datos de MONEDA
 *     Creada: 23-04-2019 
 *     Autor: Joan Marchant    
       **************************************************************************** */
        [NonAction]
        private Moneda Get_moneda(int? MO_id)
        {
            return (monedaRepository.Find(long.Parse(MO_id.ToString())));
        }


        /*     ****************************************************************************
         *     Descripcion: METODO para acceder a datos de PAIS
         *     Creada: 23-04-2019 
         *     Autor: Joan Marchant    
               **************************************************************************** */
        [NonAction]
        private Destino Get_Destino(int? destinoId)
        {
            return (destinoRepository.Find(long.Parse(destinoId.ToString())));
        }

        /*     ****************************************************************************
        *     Descripcion: METODO para acceder a datos de ESTADO
        *     Creada: 23-04-2019 
        *     Autor: Joan Marchant    
              **************************************************************************** */
        [NonAction]
        private Zona Get_Zona(int? zonaId)
        {
            return (zonaRepository.Find(long.Parse(zonaId.ToString())));
        }

        [NonAction]
        public bool SubirArchivo(IFormFile file, string ruta, string cedula, string solicitudId)
        {
            if (file == null || file.Length == 0)
                return false;

            string path = Directory.GetCurrentDirectory() + "\\wwwroot\\" + ruta + cedula + "\\" + solicitudId + "\\";

            var pathFile = Path.Combine(path, file.FileName);

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (var stream = new FileStream(pathFile, FileMode.Create))
            {
                file.CopyToAsync(stream);
            }

            return true;
        }

        [NonAction]
        public string getRuta(IFormFile file, string ruta, string cedula, string solicitudId)
        {
            string path = "~/" + ruta + cedula + "/" + solicitudId + "/";
            var pathFile = Path.Combine(path, file.FileName);
            return pathFile;
        }

        [NonAction]
        public FileStreamResult Download(string filename)
        {
            if (filename == null)
                return null;

            var path = Path.Combine(
                           Directory.GetCurrentDirectory(),
                           "wwwroot", filename);

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyToAsync(memory);
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
                {".xlsx", "application/vnd.openxmlformats  officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };
        }

        private DateTime AddBusinessDays(DateTime dt, int nDays)
        {
            int weeks = nDays / 6;
            nDays %= 6;
            while (dt.DayOfWeek == DayOfWeek.Sunday)
                dt = dt.AddDays(1);

            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Sunday)
                    dt = dt.AddDays(1);
            }
            return dt.AddDays(weeks * 7);
        }

        private int getPasoInicialFlujo()
        {
            var paso = pasoFlujoSolicitudRepository.All().Where(m => m.FlujoSolicitudId == 1 && m.Estatus == 1).OrderBy(m=>m.Orden).Select(m => m.Id).FirstOrDefault();
            return paso;
        }
        #endregion
    }
}