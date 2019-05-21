using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Data.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Engine;
using Legalizaciones.Web.Models;
using Legalizaciones.Model;
using Legalizaciones.Interface;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Web.Helpers;
using Legalizaciones.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting;

namespace Legalizaciones.Web.Controllers
{
    [Route("Legalizaciones")]
    public class LegalizacionesController : Controller
    {
        private readonly IPaisRepository paisRepository;
        private readonly ICiudadRepository ciudadRepository;
        //private readonly IMotivoRepository motivoRepository;
        private readonly ISolicitudRepository solicitudRepository;
        private readonly IEmpleadoRepository empleadoRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private readonly IBancoRepository bancoRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly ILegalizacionRepository legalizacionRepository;
        private readonly ILegalizacionGastosRepository legalizacionGastosRepository;
        private readonly ITasaRepository tasaRepository;
        private readonly IHostingEnvironment env;

        public UNOEE objUNOEE = new UNOEE();

        public LegalizacionesController(ISolicitudRepository solicitudRepository,
            ISolicitudGastosRepository solicitudGastosRepository, IBancoRepository bancoRepository,
            IMonedaRepository monedaRepository, ILegalizacionRepository legalizacionRepository,
            IEmpleadoRepository empleadoRepository, ICiudadRepository ciudadRepository, ITasaRepository tasaRepository,
            IPaisRepository paisRepository, ILegalizacionGastosRepository legalizacionGastosRepository,
            IHostingEnvironment _env)
        {
            this.solicitudRepository = solicitudRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.bancoRepository = bancoRepository;
            this.monedaRepository = monedaRepository;
            this.legalizacionRepository = legalizacionRepository;
            this.legalizacionGastosRepository = legalizacionGastosRepository;
            this.empleadoRepository = empleadoRepository;
            this.empleadoRepository = empleadoRepository;
            this.ciudadRepository = ciudadRepository;
            this.paisRepository = paisRepository;
            this.tasaRepository = tasaRepository;
            this.env = _env;
        }

        public IActionResult Index()
        {
            List<InfoLegalizacion> model = new List<InfoLegalizacion>();
            EngineDb Metodo = new EngineDb();

            string usuarioCargo = HttpContext.Session.GetString("Usuario_Cargo");
            string usuarioCedula = string.Empty;
            if (usuarioCargo == "3")
            {
                model = Metodo.SolicitudesAntPendientesLegalizacion("Sp_GetSolicitudesAnticiposPendientesLegalizacion",
                    string.Empty);
            }
            else
            { 
                if (usuarioCedula != string.Empty)
                    model = Metodo.SolicitudesAntPendientesLegalizacion(
                        "Sp_GetSolicitudesAnticiposPendientesLegalizacion", usuarioCedula);
            }

            return View(model);
        }

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear(int id)
        {
            //Usuario_Cedula
            string wCedulaUsuariopordefecto = HttpContext.Session.GetString("Usuario_Cedula").ToString();
            string wUsuarioCargo = HttpContext.Session.GetString("Usuario_Cargo").ToString();

            if (wUsuarioCargo == "2")
                ViewBag.MostrarTasa = true;
            else
                ViewBag.MostrarTasa = false;

            var ListaBanco = bancoRepository.All().ToList();
            var ListaMoneda = monedaRepository.All().ToList();

            if (id == 0) //si viene con el valor "0" se refiere a una legalizacion sin anticipo. Solo debo de cargar los bancos y la moneda
            {

                var ListaEmpleado = objUNOEE.EmpleadoAll();
                var OLegalizaciones = new LegalizacionesViewModel
                {
                    DocumentoERPID = 11111,
                    ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                    ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                    ListaEmpleado = new SelectList(ListaEmpleado, "Cedula", "Nombre"),
                    ConAnticipo = 0,
                    CedulaId = wCedulaUsuariopordefecto

                };
                return View(OLegalizaciones);
            }
            else
            {
                var Osolicitud = solicitudRepository.Find(id);
                var ListsolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == id).ToList();
                var OEmpleado = objUNOEE.getEmpleadoCedula(Osolicitud.EmpleadoCedula);
                var OCentroCosto = objUNOEE.getCentroCosto(Osolicitud.CentroCostoId);
                var OCentroOperaciones = objUNOEE.getCentroOperacion(Osolicitud.CentroOperacionId);
                var OUnidadNegocio = objUNOEE.getUnidadNegocio(Osolicitud.UnidadNegocioId);
                //var ListaMotivo = objUNOEE.GetListMotivos(Osolicitud.CentroCostoId);

                var ListaCentroCosto = new List<CentroCosto>();
                var ListaCentroOperaciones = new List<CentroOperacion>();
                var ListaUnidadNegocio = new List<UnidadNegocio>();
                ListaCentroCosto.Add(OCentroCosto);
                ListaCentroOperaciones.Add(OCentroOperaciones);
                ListaUnidadNegocio.Add(OUnidadNegocio);

                string wCargo = "Empleado";
                switch (OEmpleado.CargoId)
                {
                    case 1:
                        wCargo = "Empleado";
                        break;
                    case 2:
                        wCargo = "Tesoreria";
                        break;
                    case 3:
                        wCargo = "Contabilidad";
                        break;
                }


                var OTasa = tasaRepository.All().Where(a => a.MonedaId == Osolicitud.MonedaId).FirstOrDefault();
                var wTasa = OTasa.Valor;
                long wIdMoneda = Convert.ToInt64(Osolicitud.MonedaId);
                var OMoneda = monedaRepository.Find(wIdMoneda);

                var OLegalizaciones = new LegalizacionesViewModel
                {

                    AnticipoId = Osolicitud.Id,
                    DocumentoERPID = 11111,
                    FechaRegistro = Osolicitud.FechaSolicitud,
                    FechaVencimiento = Osolicitud.FechaVencimiento,
                    Concepto = Osolicitud.Concepto,
                    Monto = Osolicitud.Monto,
                    CentroCostoDescripcion = OCentroCosto.Nombre,
                    CentroCosto = Osolicitud.CentroCostoId,
                    CentroOperacion = Osolicitud.CentroOperacionId,
                    UnidadNegocio = Osolicitud.UnidadNegocioId,
                    ListaCentroCosto = new SelectList(ListaCentroCosto, "Id", "Nombre"),
                    ListaCentroOperacion = new SelectList(ListaCentroOperaciones, "Id", "Nombre"),
                    ListaUnidadNegocio = new SelectList(ListaUnidadNegocio, "Id", "Nombre"),
                    FechaDesde = Osolicitud.FechaDesde,
                    FechaHasta = Osolicitud.FechaHasta,
                    Nombre = OEmpleado.Nombre,
                    Cedula = OEmpleado.Cedula,
                    Cargo = wCargo,
                    Area = OEmpleado.Area,
                    ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                    //ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                    MonedaId = Osolicitud.MonedaId,
                    Moneda = OMoneda.Nombre,
                    ValorTasa = wTasa,

                    //ListaMotivo = new SelectList(ListaMotivo, "Id", "Nombre"),
                    SolicitudGastos = ListsolicitudGastos,
                    ConAnticipo = 1

                };

                return View(OLegalizaciones);

            }


        }

        [HttpPost]
        [Route("Crear")]
        public ActionResult Guardar(LegalizacionesViewModel legalizacion, IFormFile file)
        {
            try
            {
                //creo mi objeto de legalizaciones del header
                var OLegalizacionHeader = new Legalizacion
                {
                    Estatus = 1,
                    FechaCreacion = DateTime.Now,
                    SolicitudID = legalizacion.AnticipoId,
                    ReciboCaja = legalizacion.ReciboCaja,
                    Consignacion = legalizacion.Consignacion,
                    Valor = legalizacion.Valor,
                    BancoId = legalizacion.BancoId,
                    EmpleadoCedula = legalizacion.CedulaId != null ? legalizacion.CedulaId : null
                };
                legalizacionRepository.Insert(OLegalizacionHeader);


                //Se actualiza el estado de la solicitud a Legalizada
                if (OLegalizacionHeader.Id != null && OLegalizacionHeader.Id > 0)
                {
                    if(legalizacion.AnticipoId != null && legalizacion.AnticipoId > 0)
                    {
                        var solicitud = solicitudRepository.Find(legalizacion.AnticipoId);
                        solicitud.EstadoId = 2; //Legalizada
                        solicitudRepository.Update(solicitud);
                    }
                }

                //creo la lista de los detalles que vienen del json recorro la lista y guardo el detalle en la bd
                var LegalizacionGastos =
                    JsonConvert.DeserializeObject<List<LegalizacionGastos>>(legalizacion.GastosJSON);
                foreach (var item in LegalizacionGastos)
                {
                    item.LegalizacionId = OLegalizacionHeader.Id;
                    item.CentroCostosId = 1;
                    item.CentroOperacionId = 1;
                    item.UnidadNegocioId = 1;

                    legalizacionGastosRepository.Insert(item);
                }


                TempData["Alerta"] = "success - La Solicitud se registro correctamente.";
                return RedirectToAction("Index", "Legalizaciones");

            }
            catch (System.Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la solicitud";
            }


            return RedirectToAction("Index");
        }


        [HttpPost]
        [Route("Actualizar")]
        public ActionResult Actualizar(LegalizacionesViewModel legalizacion, IFormFile file)
        {
            try
            {
                //creo mi objeto de legalizaciones del header
                var OLegalizacionHeader = new Legalizacion
                {
                    Id      = legalizacion.legalizacionesId,
                    Estatus = 1,
                    FechaCreacion = DateTime.Now,
                    SolicitudID = legalizacion.AnticipoId,
                    ReciboCaja = legalizacion.ReciboCaja,
                    Consignacion = legalizacion.Consignacion,
                    Valor = legalizacion.Valor,
                    BancoId = legalizacion.BancoId,
                    EmpleadoCedula = legalizacion.CedulaId != null ? legalizacion.CedulaId : null
                };

                legalizacionRepository.Update(OLegalizacionHeader);

                foreach (var item in legalizacion.LegalizacionGastos)
                {
                    legalizacionGastosRepository.Delete(item);
                }

                //creo la lista de los detalles que vienen del json recorro la lista y guardo el detalle en la bd
                var LegalizacionGastos =
                    JsonConvert.DeserializeObject<List<LegalizacionGastos>>(legalizacion.GastosJSON);
                foreach (var item in LegalizacionGastos)
                {
                    item.LegalizacionId = OLegalizacionHeader.Id;
                    item.CentroCostosId = 1;
                    item.CentroOperacionId = 1;
                    item.UnidadNegocioId = 1;

                    legalizacionGastosRepository.Insert(item);
                }

                TempData["Alerta"] = "success - La Solicitud se ha actualizado correctamente.";
                return RedirectToAction("Index", "Legalizaciones");

            }
            catch (System.Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la solicitud";
            }


            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("LegalizacionDetalles")]
        public ActionResult Ver(int Id)
        {
            Legalizacion legalizacion = legalizacionRepository.Find(Id);
            legalizacion.Solicitud = solicitudRepository.Find(legalizacion.SolicitudID);
            legalizacion.SolicitudGastos = solicitudGastosRepository.All().Where(a=> a.SolicitudId == legalizacion.SolicitudID).ToList();
            legalizacion.LegalizacionGastos =
                legalizacionGastosRepository.All().Where(a => a.LegalizacionId == Id).ToList();
            legalizacion.Empleado = objUNOEE.getEmpleadoCedula(legalizacion.Solicitud.EmpleadoCedula);

            @ViewBag.SumLega = legalizacion.LegalizacionGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Valor));
            @ViewBag.SumSol = legalizacion.SolicitudGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Monto));

            return View(legalizacion);
        }


        [HttpGet]
        [Route("EditarLegalizacion")]
        public ActionResult Editar(int Id, int legaId)
        {
            return View(CargarDataComun(Id, legaId));
        }


        private LegalizacionesViewModel CargarDataComun(int id, int legaId)
        {
            //Usuario_Cedula
            string wCedulaUsuariopordefecto = HttpContext.Session.GetString("Usuario_Cargo").ToString();

            var ListaBanco = bancoRepository.All().ToList();
            var ListaMoneda = monedaRepository.All().ToList();

            if (id == 0) //si viene con el valor "0" se refiere a una legalizacion sin anticipo. Solo debo de cargar los bancos y la moneda
            {
                var ListaEmpleado = objUNOEE.EmpleadoAll();
                var OLegalizaciones = new LegalizacionesViewModel
                {
                    DocumentoERPID = 11111,
                    ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                    ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                    ListaEmpleado = new SelectList(ListaEmpleado, "Cedula", "Nombre"),
                    ConAnticipo = 0,
                    CedulaId = wCedulaUsuariopordefecto

                };
                return OLegalizaciones;
            }
            else
            {
                var Osolicitud = solicitudRepository.Find(id);
                //var ListsolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == id).ToList();
                var ListLegalizacionGastos = legalizacionGastosRepository.All().Where(a => a.LegalizacionId == legaId).ToList();
                foreach (var item in ListLegalizacionGastos)
                {
                    item.Pais   = paisRepository.Find(item.PaisId);
                    item.Ciudad = ciudadRepository.Find(item.CiudadId);
                    item.CentroOperacion = new CentroOperacion
                    {
                        Id = 1,
                        Nombre = "Centro Operacion"
                    };
                    item.CentroCosto = new CentroCosto
                    {
                        Id = 1,
                        Nombre = "Centro de Costo 1"
                    };
                    item.UnidadNegocio = new UnidadNegocio
                    {
                        Id = 1,
                        Nombre = "Unidad Neg 1"
                    };
                    //item.Motivo = motivoRepository.Find(long.Parse(item.MotivoId.ToString()));
                }
                var OEmpleado = objUNOEE.getEmpleadoCedula(Osolicitud.EmpleadoCedula);

                string wCargo = "Empleado";
                switch (OEmpleado.CargoId)
                {
                    case 1:
                        wCargo = "Empleado";
                        break;
                    case 2:
                        wCargo = "Tesoreria";
                        break;
                    case 3:
                        wCargo = "Contabilidad";
                        break;
                }

                var OLegalizaciones = new LegalizacionesViewModel
                {
                    AnticipoId = Osolicitud.Id,
                    DocumentoERPID = 11111,
                    FechaRegistro = Osolicitud.FechaSolicitud,
                    FechaVencimiento = Osolicitud.FechaVencimiento,
                    Concepto = Osolicitud.Concepto,
                    Monto = Osolicitud.Monto,
                    FechaDesde = Osolicitud.FechaDesde,
                    FechaHasta = Osolicitud.FechaHasta,
                    Nombre = OEmpleado.Nombre,
                    Cedula = OEmpleado.Cedula,
                    Cargo = wCargo,
                    Area = OEmpleado.Area,
                    ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                    ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                    MonedaId = Osolicitud.MonedaId,
                    LegalizacionGastos = ListLegalizacionGastos,
                    ConAnticipo = 1,
                    ReciboCaja   = legalizacionRepository.Find(long.Parse(legaId.ToString())).ReciboCaja,
                    Consignacion = legalizacionRepository.Find(long.Parse(legaId.ToString())).Consignacion,
                    Valor = legalizacionRepository.Find(long.Parse(legaId.ToString())).Valor
                   
                };

                return OLegalizaciones;

            }
        }

        [HttpGet]
        [Route("VisorLegalizacionPDF")]
        public ActionResult VisorLegalizacionPDF(int id)
        {
            Legalizacion legalizacion = legalizacionRepository.Find(id);
            legalizacion.Solicitud = solicitudRepository.Find(legalizacion.SolicitudID);
            legalizacion.SolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == legalizacion.SolicitudID).ToList();
            legalizacion.LegalizacionGastos =
                legalizacionGastosRepository.All().Where(a => a.LegalizacionId == id).ToList();
            legalizacion.Empleado = objUNOEE.getEmpleadoCedula(legalizacion.Solicitud.EmpleadoCedula);

            @ViewBag.SumLega = legalizacion.LegalizacionGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Valor));
            @ViewBag.SumSol = legalizacion.SolicitudGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Monto));

            return View(legalizacion);
        }

        
    }
}