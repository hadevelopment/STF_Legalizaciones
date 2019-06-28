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
using Legalizaciones.Model.Empresa;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;
using Legalizaciones.Model.Workflow;

namespace Legalizaciones.Web.Controllers
{
    [Route("Legalizaciones")]
    public class LegalizacionesController : Controller
    {
        private readonly IPaisRepository paisRepository;
        private readonly ICiudadRepository ciudadRepository;
        //private readonly IMotivoRepository motivoRepository;
        private readonly ISolicitudRepository solicitudRepository;
        private readonly IEstadoSolicitudRepository estadoSolicitudRepository;
        private readonly IEstadoLegalizacionRepository estadoLegalizacionRepository;
        private readonly IKactusEmpleadoRepository kactusEmpleadoRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private readonly IBancoRepository bancoRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly ILegalizacionRepository legalizacionRepository;
        private readonly ILegalizacionGastosRepository legalizacionGastosRepository;
        private readonly ILegalizacionAprobacionRepository legalizacionAprobacionRepository;
        private readonly ITasaRepository tasaRepository;

        /*WORKFLOW*/
        private readonly IPasoFlujoSolicitudRepository pasoFlujoSolicitudRepository;
        private readonly IFlujoSolicitudRepository flujoSolicitudRepository;
        private readonly ITipoSolicitudRepository tipoSolicitudRepository;

        private readonly IEmail email;

        public UNOEE objUNOEE = new UNOEE();

        /*DB*/
        EngineDb DB = new EngineDb();

        public LegalizacionesController(ISolicitudRepository solicitudRepository,
            ISolicitudGastosRepository solicitudGastosRepository, IBancoRepository bancoRepository,
            IMonedaRepository monedaRepository, ILegalizacionRepository legalizacionRepository,
            IKactusEmpleadoRepository kactusEmpleadoRepository, ICiudadRepository ciudadRepository, ITasaRepository tasaRepository,
            IPaisRepository paisRepository, ILegalizacionGastosRepository legalizacionGastosRepository,
            IPasoFlujoSolicitudRepository pasoFlujoSolicitudRepository,
            IFlujoSolicitudRepository flujoSolicitudRepository,
            IEmail email,
            IEstadoSolicitudRepository estadoSolicitudRepository,
            ITipoSolicitudRepository tipoSolicitudRepository,
            ILegalizacionAprobacionRepository legalizacionAprobacionRepository,
            IEstadoLegalizacionRepository estadoLegalizacionRepository)
        {
            this.solicitudRepository = solicitudRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.bancoRepository = bancoRepository;
            this.monedaRepository = monedaRepository;
            this.legalizacionRepository = legalizacionRepository;
            this.legalizacionGastosRepository = legalizacionGastosRepository;
            this.kactusEmpleadoRepository = kactusEmpleadoRepository;
            this.ciudadRepository = ciudadRepository;
            this.paisRepository = paisRepository;
            this.tasaRepository = tasaRepository;
            this.pasoFlujoSolicitudRepository = pasoFlujoSolicitudRepository;
            this.flujoSolicitudRepository = flujoSolicitudRepository;
            this.email = email;
            this.estadoSolicitudRepository = estadoSolicitudRepository;
            this.tipoSolicitudRepository = tipoSolicitudRepository;
            this.legalizacionAprobacionRepository = legalizacionAprobacionRepository;
            this.estadoLegalizacionRepository = estadoLegalizacionRepository;
        }

        public IActionResult Index()
        {
            List<InfoLegalizacion> model = new List<InfoLegalizacion>();
            EngineDb Metodo = new EngineDb();

            string usuarioRol = HttpContext.Session.GetString("Usuario_Rol");
            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");

            if (usuarioRol == "Contraloria" || usuarioRol == "Administrador")
            {
                model = Metodo.SolicitudesAntPendientesLegalizacion(string.Empty);
            }
            else
            {
                if (usuarioCedula != string.Empty)
                    model = Metodo.SolicitudesAntPendientesLegalizacion(usuarioCedula);
            }

            return View(model);
        }

        [HttpPost]
        [Route("")]
        public ActionResult Filtrar(DateTime fechaDesde, DateTime fechaHasta)
        {
            List<InfoLegalizacion> model = new List<InfoLegalizacion>();
            EngineDb Metodo = new EngineDb();

            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");

            if (!string.IsNullOrEmpty(usuarioCedula))
            {
                model = Metodo.SolicitudesAntPendientesLegalizacionFiltrar(usuarioCedula, fechaDesde, fechaHasta);
                return View("Index", model);
            }
            else
            {
                TempData["Alerta"] = "error - No se pudo completar el filtro, no se obtuvo el dato cédula de la session.";
                return View("Index", null);
            }
        }

        private async Task<List<KactusIntegration.Empleado>> GetKactusEmpleadoAsync()
        {
            EngineStf Funcion = new EngineStf();
            List<KactusIntegration.Empleado> empleado = new List<KactusIntegration.Empleado>();
            empleado = await Funcion.EmpleadoKactusAsync();
            EngineDb Metodo = new EngineDb();
            Metodo.InsertKactusEmpleados("Sp_InsertKactusEmpleado", empleado);
            return empleado;
        }

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear(int id)
        {
            // Obtenemos datos del empleado en Session
            string cedula = "";
            string cargo = "";
            string rol = "";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cedula")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Rol")))
            {
                cedula = HttpContext.Session.GetString("Usuario_Cedula");
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
                rol = HttpContext.Session.GetString("Usuario_Rol");

                /*Validar Perfil*/
                if (rol == "Tesoreria")
                    ViewBag.MostrarTasa = true;
                else
                    ViewBag.MostrarTasa = false;


                /*Cargar Listas*/
                var ListaBanco = bancoRepository.All().ToList();
                var ListaCentroCosto = objUNOEE.getCentroCostos();
                var ListaCentroOperaciones = objUNOEE.getCentroOperaciones();
                var ListaUnidadNegocio = objUNOEE.getUnidadNegocios();


                if (id == 0) //si viene con el valor "0" se refiere a una legalizacion sin anticipo. Solo debo de cargar los bancos y la moneda
                {
                    var ListaMoneda = monedaRepository.All().ToList();
                    var ListaEmpleado = kactusEmpleadoRepository.All();

                    var OEmpleado = kactusEmpleadoRepository.getEmpleadoCedula(cedula);

                    var OLegalizaciones = new LegalizacionesViewModel
                    {
                        DocumentoERPID = "N/A",
                        ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                        ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                        ListaEmpleado = new SelectList(ListaEmpleado, "Cedula", "Nombre"),
                        ConAnticipo = 0,
                        Empleado = OEmpleado,
                        ListaCentroCosto = new SelectList(ListaCentroCosto, "Id", "Nombre"),
                        ListaCentroOperacion = new SelectList(ListaCentroOperaciones, "Id", "Nombre"),
                        ListaUnidadNegocio = new SelectList(ListaUnidadNegocio, "Id", "Nombre"),
                    };
                    return View(OLegalizaciones);
                }
                else
                {
                    var Osolicitud = solicitudRepository.Find(id);
                    var ListsolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == id).ToList();
                    var OEmpleado = kactusEmpleadoRepository.getEmpleadoCedula(Osolicitud.EmpleadoCedula);


                    var OTasa = tasaRepository.All().Where(a => a.MonedaId == Osolicitud.MonedaId).FirstOrDefault();
                    var wTasa = OTasa.Valor;
                    long wIdMoneda = Convert.ToInt64(Osolicitud.MonedaId);
                    var OMoneda = monedaRepository.Find(wIdMoneda);

                    var OLegalizaciones = new LegalizacionesViewModel
                    {
                        AnticipoId = Osolicitud.Id,
                        DocumentoERPID = Osolicitud.DocumentoERP,
                        FechaRegistro = Osolicitud.FechaSolicitud,
                        FechaVencimiento = Osolicitud.FechaVencimiento,
                        Concepto = Osolicitud.Concepto,
                        Monto = Osolicitud.Monto,
                        CentroCostoDescripcion = Osolicitud.CentroCosto,
                        CentroCosto = Osolicitud.CentroCostoId,
                        CentroOperacion = Osolicitud.CentroOperacionId,
                        UnidadNegocio = Osolicitud.UnidadNegocioId,
                        ListaCentroCosto = new SelectList(ListaCentroCosto, "Id", "Nombre"),
                        ListaCentroOperacion = new SelectList(ListaCentroOperaciones, "Id", "Nombre"),
                        ListaUnidadNegocio = new SelectList(ListaUnidadNegocio, "Id", "Nombre"),
                        FechaDesde = Osolicitud.FechaDesde,
                        FechaHasta = Osolicitud.FechaHasta,
                        Empleado = OEmpleado,
                        ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                        ReciboCaja = 0,
                        Consignacion = 0,
                        Valor = "0",
                        MonedaId = Osolicitud.MonedaId,
                        Moneda = OMoneda.Nombre,
                        ValorTasa = wTasa,
                        SolicitudGastos = ListsolicitudGastos,
                        ConAnticipo = 1,
                        MontoAnticipoEntregado = Osolicitud.Monto
                    };

                    return View(OLegalizaciones);
                }
            }

            return View(null);

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
                    Empleado = legalizacion.Empleado,
                    EmpleadoNombre = legalizacion.Empleado.PrimerNombre + " " + legalizacion.Empleado.PrimerApellido,
                    EmpleadoCedula = legalizacion.Empleado.NumeroDeIdentificacion != null ? legalizacion.Empleado.NumeroDeIdentificacion : null,
                    MontoAnticipoEntregado = legalizacion.MontoAnticipoEntregado,
                    MontoGastosReportados = legalizacion.MontoGastosReportados,
                    MontoSaldo = legalizacion.MontoSaldo,
                    EstadoId = estadoLegalizacionRepository.All().Where(m => m.Descripcion.Contains("Creada")).Select(m => m.Id).FirstOrDefault(), //Estado Inicial
                    FechaDesde = legalizacion.FechaDesde,
                    FechaHasta = legalizacion.FechaHasta,
                    DestinoID = legalizacion.DestinoID
                };

                //Si viene con anticipo se toma el destino del anticipo
                if(legalizacion.AnticipoId > 0)
                {
                    var solicitud = solicitudRepository.Find(legalizacion.AnticipoId);

                    //Se valida que exista un flujo para la legalizacion y se obtiene el paso inicial del flujo configurado
                    if (!getPasoInicialFlujo(OLegalizacionHeader, solicitud.DestinoID, (float)OLegalizacionHeader.MontoGastosReportados))
                    {
                        TempData["Alerta"] = "warning - No hay un flujo de aprobación creado para esta legalización. Comuníquese con el administrador de sistema.";
                        return RedirectToAction("Crear", routeValues: new { id = OLegalizacionHeader.SolicitudID });
                    }

                    //Se Registra La Legalizacion
                    legalizacionRepository.Insert(OLegalizacionHeader);

                    //Se actualiza el estado de la solicitud a Legalizada
                    if (OLegalizacionHeader.Id > 0)
                    {
                        if (legalizacion.AnticipoId > 0)
                        {
                            var estadoId = estadoSolicitudRepository.All().Where(m => m.Descripcion == "En Proceso de Legalización" && m.Estatus == 1).Select(z => z.Id).FirstOrDefault();
                            solicitud.EstadoId = estadoId; //Legalizada
                            solicitudRepository.Update(solicitud);
                        }
                    }
                }
                //Sino se toma de la legalización
                else
                {
                    //Se valida que exista un flujo para la legalizacion y se obtiene el paso inicial del flujo configurado
                    if (!getPasoInicialFlujo(OLegalizacionHeader, OLegalizacionHeader.DestinoID, (float)OLegalizacionHeader.MontoGastosReportados))
                    {
                        TempData["Alerta"] = "warning - No hay un flujo de aprobación creado para esta legalización. Comuníquese con el administrador de sistema.";
                        return RedirectToAction("Crear", routeValues: new { id = OLegalizacionHeader.SolicitudID });
                    }

                    //Se Registra La Legalizacion
                    legalizacionRepository.Insert(OLegalizacionHeader);
                }

                //Aplico Formato JSON a los thead que vienen de la tabla
                legalizacion.GastosJSON = TableToJSON(legalizacion.GastosJSON);

                //creo la lista de los detalles que vienen del json recorro la lista y guardo el detalle en la bd
                var LegalizacionGastos = JsonConvert.DeserializeObject<List<DecerializeLegalizacionGasto>>(legalizacion.GastosJSON);
                foreach (var item in LegalizacionGastos)
                {
                    var wOLegalizacionGasto = new LegalizacionGastos
                    {
                        Concepto = item.ConceptoGasto,
                        FechaCreacion = DateTime.Now,
                        FechaGasto = item.FechaGasto,
                        LegalizacionId = OLegalizacionHeader.Id,
                        CentroOperacionId = item.CentroOperacionId,
                        UnidadNegocioId = item.UnidadNegocioId,
                        CentroCostoId = item.CentroCostoId,
                        CentroOperacion = item.CentroOperacion,
                        UnidadNegocio = item.UnidadNegocio,
                        CentroCosto = item.CentroCosto,
                        MotivoId = 0,
                        PaisId = item.PaisId,
                        Pais = item.Pais,
                        CiudadId = item.CiudadId,
                        Ciudad = item.Ciudad,
                        TipoServicioId = item.TipoServicioId,
                        Servicio = item.Servicio,
                        ProveedorId = item.ProveedorId,
                        Valor = item.Valor,
                        Estatus = 1
                    };
                    legalizacionGastosRepository.Insert(wOLegalizacionGasto);
                }

                //Se Registra el Flujo de Aprobacion de la Legalizacion
                crearFlujoAprobacionLegalizacion(OLegalizacionHeader);

                TempData["Alerta"] = "success - La Legalización se proceso correctamente.";
                return RedirectToAction("Index", "Legalizaciones");
            }
            catch (System.Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de procesar la Legalización.";
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
                    Id = legalizacion.legalizacionesId,
                    Estatus = 1,
                    FechaCreacion = DateTime.Now,
                    SolicitudID = legalizacion.AnticipoId,
                    ReciboCaja = legalizacion.ReciboCaja,
                    Consignacion = legalizacion.Consignacion,
                    Valor = legalizacion.Valor,
                    BancoId = legalizacion.BancoId,
                    Empleado = legalizacion.Empleado,
                    EmpleadoNombre = legalizacion.Empleado.PrimerNombre + " " + legalizacion.Empleado.PrimerApellido,
                    EmpleadoCedula = legalizacion.Empleado.NumeroDeIdentificacion != null ? legalizacion.Empleado.NumeroDeIdentificacion : null,
                    MontoAnticipoEntregado = legalizacion.MontoAnticipoEntregado,
                    MontoGastosReportados = legalizacion.MontoGastosReportados,
                    MontoSaldo = legalizacion.MontoSaldo,
                    EstadoId = estadoLegalizacionRepository.All().Where(m => m.Descripcion.Contains("Creada")).Select(m => m.Id).FirstOrDefault(), //Estado Inicial
                    FechaDesde = legalizacion.FechaDesde,
                    FechaHasta = legalizacion.FechaHasta,
                    DestinoID = legalizacion.DestinoID
                };

                //Si viene con anticipo se toma el destino del anticipo
                if (legalizacion.AnticipoId > 0)
                {
                    var solicitud = solicitudRepository.Find(legalizacion.AnticipoId);

                    //Se valida que exista un flujo para la legalizacion y se obtiene el paso inicial del flujo configurado
                    if (!getPasoInicialFlujo(OLegalizacionHeader, solicitud.DestinoID, (float)OLegalizacionHeader.MontoGastosReportados))
                    {
                        TempData["Alerta"] = "warning - No hay un flujo de aprobación creado para esta legalización. Comuníquese con el administrador de sistema.";
                        return RedirectToAction("Crear", routeValues: new { id = OLegalizacionHeader.SolicitudID });
                    }

                    //Se Registra La Legalizacion
                    legalizacionRepository.Update(OLegalizacionHeader);

                    //Se actualiza el estado de la solicitud a Legalizada
                    if (OLegalizacionHeader.Id > 0)
                    {
                        if (legalizacion.AnticipoId > 0)
                        {
                            var estadoId = estadoSolicitudRepository.All().Where(m => m.Descripcion == "En Proceso de Legalización" && m.Estatus == 1).Select(z => z.Id).FirstOrDefault();
                            solicitud.EstadoId = estadoId; //Legalizada
                            solicitudRepository.Update(solicitud);
                        }
                    }
                }
                //Sino se toma de la legalización
                else
                {
                    //Se valida que exista un flujo para la legalizacion y se obtiene el paso inicial del flujo configurado
                    if (!getPasoInicialFlujo(OLegalizacionHeader, OLegalizacionHeader.DestinoID, (float)OLegalizacionHeader.MontoGastosReportados))
                    {
                        TempData["Alerta"] = "warning - No hay un flujo de aprobación creado para esta legalización. Comuníquese con el administrador de sistema.";
                        return RedirectToAction("Crear", routeValues: new { id = OLegalizacionHeader.SolicitudID });
                    }

                    //Se Registra La Legalizacion
                    legalizacionRepository.Update(OLegalizacionHeader);
                }

                //Limpio los gastos previos registrados
                var gastosLegalizacionDatosBD = legalizacionGastosRepository.All().Where(m => m.LegalizacionId == OLegalizacionHeader.Id).ToList();
                foreach (var item in gastosLegalizacionDatosBD)
                {
                    legalizacionGastosRepository.Delete(item);
                }

                //Aplico Formato JSON a los thead que vienen de la tabla
                legalizacion.GastosJSON = TableToJSON(legalizacion.GastosJSON);
                //creo la lista de los detalles que vienen del json recorro la lista y guardo el detalle en la bd
                var LegalizacionGastos = JsonConvert.DeserializeObject<List<DecerializeLegalizacionGasto>>(legalizacion.GastosJSON);
                foreach (var item in LegalizacionGastos)
                {
                    var wOLegalizacionGasto = new LegalizacionGastos
                    {
                        Concepto = item.ConceptoGasto,
                        FechaCreacion = DateTime.Now,
                        FechaGasto = item.FechaGasto,
                        LegalizacionId = OLegalizacionHeader.Id,
                        CentroOperacionId = item.CentroOperacionId,
                        UnidadNegocioId = item.UnidadNegocioId,
                        CentroCostoId = item.CentroCostoId,
                        CentroOperacion = item.CentroOperacion,
                        UnidadNegocio = item.UnidadNegocio,
                        CentroCosto = item.CentroCosto,
                        MotivoId = 0,
                        PaisId = item.PaisId,
                        Pais = item.Pais,
                        CiudadId = item.CiudadId,
                        Ciudad = item.Ciudad,
                        TipoServicioId = item.TipoServicioId,
                        Servicio = item.Servicio,
                        ProveedorId = item.ProveedorId,
                        Valor = item.Valor,
                        Estatus = 1
                    };
                    legalizacionGastosRepository.Insert(wOLegalizacionGasto);
                }

                //Se Registra el Flujo de Aprobacion de la Legalizacion
                crearFlujoAprobacionLegalizacion(OLegalizacionHeader);

                TempData["Alerta"] = "success - La Legalización se ha actualizado correctamente.";
                return RedirectToAction("Index", "Legalizaciones");
            }
            catch (System.Exception e)
            {
                TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de actualizar la Legalización";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Detalle")]
        public ActionResult Visualizar(int Id)
        {
            Legalizacion legalizacion = legalizacionRepository.Find(Id);
            legalizacion.Solicitud = solicitudRepository.Find(legalizacion.SolicitudID);
            legalizacion.SolicitudGastos = solicitudGastosRepository.All().Where(a=> a.SolicitudId == legalizacion.SolicitudID).ToList();
            legalizacion.LegalizacionGastos = legalizacionGastosRepository.All().Where(a => a.LegalizacionId == Id).ToList();
            legalizacion.Empleado = kactusEmpleadoRepository.getEmpleadoCedula(legalizacion.EmpleadoCedula);

            List<Flujo> lstFlujo = new List<Flujo>();
            lstFlujo = DB.ObtenerFlujoLegalizacion(Id);

            if (lstFlujo.Count > 0)
                legalizacion.ListaFlujo = lstFlujo.OrderBy(m => m.Orden).ToList();

            @ViewBag.SumLega = legalizacion.LegalizacionGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Valor));
            @ViewBag.SumSol  = legalizacion.SolicitudGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Monto));

            return View(legalizacion);
        }


        [HttpGet]
        [Route("EditarLegalizacion")]
        public ActionResult Editar(int Id, int legaId)
        {
            //Usuario_Cedulastring cedula = "";
            string cedula = "";
            string cargo = "";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cedula")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cedula = HttpContext.Session.GetString("Usuario_Cedula");
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            var ListaBanco = bancoRepository.All().ToList();
            var ListaMoneda = monedaRepository.All().ToList();
            var ListaCentroCosto = objUNOEE.getCentroCostos();
            var ListaCentroOperaciones = objUNOEE.getCentroOperaciones();
            var ListaUnidadNegocio = objUNOEE.getUnidadNegocios();

            var solicitud = solicitudRepository.Find(Id);
            var solicitudGastos = solicitudGastosRepository.All().Where(m => m.SolicitudId == Id).ToList();
            var legalizacion = legalizacionRepository.Find(legaId);
            var legalizacionGastos = legalizacionGastosRepository.All().Where(m => m.LegalizacionId == legaId).ToList();
            var empleado = kactusEmpleadoRepository.All().Where(m => m.NumeroDeIdentificacion == legalizacion.EmpleadoCedula).Single();

            if(legalizacion != null)
            {
                if (solicitud != null)
                {
                    var moneda = monedaRepository.All().Where(m=>m.Id == solicitud.MonedaId).Single();

                    var OLegalizaciones = new LegalizacionesViewModel
                    {
                        legalizacionesId = legalizacion.Id,
                        AnticipoId = solicitud.Id,
                        DocumentoERPID = solicitud.DocumentoERP,
                        FechaRegistro = solicitud.FechaSolicitud,
                        FechaVencimiento = solicitud.FechaVencimiento,
                        Concepto = solicitud.Concepto,
                        Monto = solicitud.Monto,
                        FechaDesde = solicitud.FechaDesde,
                        FechaHasta = solicitud.FechaHasta,
                        Empleado = empleado,
                        ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                        ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                        MonedaId = solicitud.MonedaId,
                        SolicitudGastos = solicitudGastos,
                        LegalizacionGastos = legalizacionGastos,
                        ConAnticipo = 1,
                        ReciboCaja = legalizacion.ReciboCaja,
                        Consignacion = legalizacion.Consignacion,
                        Valor = legalizacion.Valor,
                        MontoAnticipoEntregado = legalizacion.MontoAnticipoEntregado,
                        MontoGastosReportados = legalizacion.MontoGastosReportados,
                        MontoSaldo = legalizacion.MontoSaldo,
                        CentroCosto = solicitud.CentroCostoId,
                        CentroCostoDescripcion = solicitud.CentroCosto,
                        ListaCentroCosto = new SelectList(ListaCentroCosto, "Id", "Nombre"),
                        ListaCentroOperacion = new SelectList(ListaCentroOperaciones, "Id", "Nombre"),
                        ListaUnidadNegocio = new SelectList(ListaUnidadNegocio, "Id", "Nombre"),
                        Moneda = moneda.Abreviatura
                    };

                    return View(OLegalizaciones);
                }
                else
                {
                    var OLegalizaciones = new LegalizacionesViewModel
                    {
                        legalizacionesId = legalizacion.Id,
                        AnticipoId = 0,
                        FechaDesde = legalizacion.FechaDesde,
                        FechaHasta = legalizacion.FechaHasta,
                        Empleado = empleado,
                        ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                        ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                        LegalizacionGastos = legalizacionGastos,
                        ConAnticipo = 0,
                        ReciboCaja = legalizacion.ReciboCaja,
                        Consignacion = legalizacion.Consignacion,
                        Valor = legalizacion.Valor,
                        MontoAnticipoEntregado = legalizacion.MontoAnticipoEntregado,
                        MontoGastosReportados = legalizacion.MontoGastosReportados,
                        MontoSaldo = legalizacion.MontoSaldo,
                        ListaCentroCosto = new SelectList(ListaCentroCosto, "Id", "Nombre"),
                        ListaCentroOperacion = new SelectList(ListaCentroOperaciones, "Id", "Nombre"),
                        ListaUnidadNegocio = new SelectList(ListaUnidadNegocio, "Id", "Nombre"),
                    };

                    return View(OLegalizaciones);
                }
            }
            else
            {
                TempData["Alerta"] = "error - No se encontro la legalización con ese Identificador.";
                return RedirectToAction("Index", "Legalizaciones");
            }
            //return View(CargarDataComun(Id, legaId));
        }

        private LegalizacionesViewModel CargarDataComun(int id, int legaId)
        {
            //Usuario_Cedulastring cedula = "";
            string cedula = "";
            string cargo = "";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cedula")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cedula = HttpContext.Session.GetString("Usuario_Cedula");
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            var ListaBanco = bancoRepository.All().ToList();
            var ListaMoneda = monedaRepository.All().ToList();

            if (id == 0) //si viene con el valor "0" se refiere a una legalizacion sin anticipo. Solo debo de cargar los bancos y la moneda
            {
                var ListaEmpleado = kactusEmpleadoRepository.All();
                var OEmpleado = kactusEmpleadoRepository.getEmpleadoCedula(cedula);

                var OLegalizaciones = new LegalizacionesViewModel
                {
                    DocumentoERPID = "N/A",
                    ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                    ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                    ListaEmpleado = new SelectList(ListaEmpleado, "Cedula", "Nombre"),
                    ConAnticipo = 0,
                    Empleado = OEmpleado

                };
                return OLegalizaciones;
            }
            else
            {
                var Osolicitud = solicitudRepository.Find(id);
                //var ListsolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == id).ToList();
                var ListLegalizacionGastos = legalizacionGastosRepository.All().Where(a => a.LegalizacionId == legaId).ToList();
                var OEmpleado = kactusEmpleadoRepository.getEmpleadoCedula(Osolicitud.EmpleadoCedula);

                var OLegalizaciones = new LegalizacionesViewModel
                {
                    AnticipoId = Osolicitud.Id,
                    DocumentoERPID = Osolicitud.DocumentoERP,
                    FechaRegistro = Osolicitud.FechaSolicitud,
                    FechaVencimiento = Osolicitud.FechaVencimiento,
                    Concepto = Osolicitud.Concepto,
                    Monto = Osolicitud.Monto,
                    FechaDesde = Osolicitud.FechaDesde,
                    FechaHasta = Osolicitud.FechaHasta,
                    Empleado = OEmpleado,
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
            var legalizacion = legalizacionRepository.Find(id);
            legalizacion.Solicitud = solicitudRepository.Find(legalizacion.SolicitudID);
            legalizacion.SolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == legalizacion.SolicitudID).ToList();
            legalizacion.LegalizacionGastos =
                legalizacionGastosRepository.All().Where(a => a.LegalizacionId == id).ToList();
            legalizacion.Empleado = kactusEmpleadoRepository.getEmpleadoCedula(legalizacion.Solicitud.EmpleadoCedula);

            @ViewBag.SumLega = legalizacion.LegalizacionGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Valor));
            @ViewBag.SumSol = legalizacion.SolicitudGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Monto));

            return View(legalizacion);
        }


        private bool getPasoInicialFlujo(Legalizacion legalizacion, int? destino, float monto)
        {
            //Obtengo el Id del flujo
            var tipoSolicitud = tipoSolicitudRepository.All().Where(m => m.Descripcion == "Solicitud de Legalizacion" ).Select(m => m.Id).FirstOrDefault();
            var idFlujo = flujoSolicitudRepository.All().Where(m => m.TipoSolicitudId == tipoSolicitud && m.DestinoId == destino && monto >= m.MontoMinimo && monto <= m.MontoMaximo).Select(m => m.Id).LastOrDefault();

            if (idFlujo != null && idFlujo > 0)
            {
                var idPaso = pasoFlujoSolicitudRepository.All().Where(m => m.FlujoSolicitudId == idFlujo && m.Estatus == 1).OrderBy(m => m.Orden).Select(m => m.Id).FirstOrDefault();

                if (legalizacion.FlujoSolicitudId != null && legalizacion.PasoFlujoSolicitudId != null)
                {
                    //Si el flujo y el paso de la legalizacion son distintos a los anteriores se debe ejecutar el trigger de la base da datos
                    if (legalizacion.FlujoSolicitudId != idFlujo && legalizacion.PasoFlujoSolicitudId != idPaso)
                    {
                        DB.TriggerActualizacionLegalizacion(legalizacion.Id);
                    }
                }

                legalizacion.FlujoSolicitudId = idFlujo;
                legalizacion.PasoFlujoSolicitudId = idPaso;
                return true;
            }
            else
            {
                return false;
            }
        }

        private void crearFlujoAprobacionLegalizacion(Legalizacion legalizacion)
        {
            //Obtengo el Id del flujo
            var tipoSolicitud = tipoSolicitudRepository.All().Where(m => m.Descripcion == "Solicitud de Legalizacion").Select(m => m.Id).FirstOrDefault();
            var pasosLegalizacion = pasoFlujoSolicitudRepository.All().Where(m => m.FlujoSolicitudId == legalizacion.FlujoSolicitudId && m.Estatus == 1).OrderBy(m => m.Orden).ToList();

            //Se registran los aprobadores de la legalizacion
            if (pasosLegalizacion.Count > 0)
            {
                //Obtengo los pasos asignados a la legalizacion para limpiarlos
                var pasosAsignados = legalizacionAprobacionRepository.All().Where(m => m.LegalizacionId == legalizacion.Id).ToList();
                if (pasosAsignados != null && pasosAsignados.Count > 0)
                {
                    foreach (var paso in pasosAsignados)
                    {
                        legalizacionAprobacionRepository.Delete(paso);
                    }
                }

                var empleado = kactusEmpleadoRepository.All().Where(m => m.NumeroDeIdentificacion == legalizacion.EmpleadoCedula).Single();
                foreach (var paso in pasosLegalizacion)
                {
                    LegalizacionAprobacion legalizacionAprobacion = new LegalizacionAprobacion();
                    legalizacionAprobacion.LegalizacionId = legalizacion.Id;
                    legalizacionAprobacion.Orden = paso.Orden;
                    legalizacionAprobacion.PasoFlujoSolicitudId = paso.Id;

                    if (paso.NivelAprobador == "NombreNivel3")
                        legalizacionAprobacion.Aprobador = empleado.NombreNivel3;

                    if (paso.NivelSuplenteUno == "NombreNivel3")
                        legalizacionAprobacion.SuplenteUno = empleado.NombreNivel3;

                    if (paso.NivelSuplenteDos == "NombreNivel3")
                        legalizacionAprobacion.SuplenteDos = empleado.NombreNivel3;

                    if (paso.NivelAprobador == "NombreNivel4")
                        legalizacionAprobacion.Aprobador = empleado.NombreNivel4;

                    if (paso.NivelSuplenteUno == "NombreNivel4")
                        legalizacionAprobacion.SuplenteUno = empleado.NombreNivel4;

                    if (paso.NivelSuplenteDos == "NombreNivel4")
                        legalizacionAprobacion.SuplenteDos = empleado.NombreNivel4;

                    if (paso.NivelAprobador == "NombreNivel5")
                        legalizacionAprobacion.Aprobador = empleado.NombreNivel5;

                    if (paso.NivelSuplenteUno == "NombreNivel5")
                        legalizacionAprobacion.SuplenteUno = empleado.NombreNivel5;

                    if (paso.NivelSuplenteDos == "NombreNivel5")
                        legalizacionAprobacion.SuplenteDos = empleado.NombreNivel5;

                    legalizacionAprobacionRepository.Insert(legalizacionAprobacion);
                }
            }
        }

        private string TableToJSON(string json)
        {
            json = json.Replace("Centro Operación", "CentroOperacion")
                        .Replace("Unidad Negocio", "UnidadNegocio")
                        .Replace("Centro Costo", "CentroCosto")
                        .Replace("Fecha Gasto", "FechaGasto")
                        .Replace("Concepto Gasto", "ConceptoGasto");
            return json;
        }


    }
}