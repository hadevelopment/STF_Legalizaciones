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

        /*WORKFLOW*/
        private readonly IPasoFlujoSolicitudRepository pasoFlujoSolicitudRepository;
        private readonly IFlujoSolicitudRepository flujoSolicitudRepository;

        private readonly IEmail email;

        public UNOEE objUNOEE = new UNOEE();

        public LegalizacionesController(ISolicitudRepository solicitudRepository,
            ISolicitudGastosRepository solicitudGastosRepository, IBancoRepository bancoRepository,
            IMonedaRepository monedaRepository, ILegalizacionRepository legalizacionRepository,
            IEmpleadoRepository empleadoRepository, ICiudadRepository ciudadRepository, ITasaRepository tasaRepository,
            IPaisRepository paisRepository, ILegalizacionGastosRepository legalizacionGastosRepository,
            IPasoFlujoSolicitudRepository pasoFlujoSolicitudRepository,
            IFlujoSolicitudRepository flujoSolicitudRepository,
            IEmail email)
        {
            this.solicitudRepository = solicitudRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.bancoRepository = bancoRepository;
            this.monedaRepository = monedaRepository;
            this.legalizacionRepository = legalizacionRepository;
            this.legalizacionGastosRepository = legalizacionGastosRepository;
            this.empleadoRepository = empleadoRepository;
            this.ciudadRepository = ciudadRepository;
            this.paisRepository = paisRepository;
            this.tasaRepository = tasaRepository;
            this.pasoFlujoSolicitudRepository = pasoFlujoSolicitudRepository;
            this.flujoSolicitudRepository = flujoSolicitudRepository;
            this.email = email;
        }

        public IActionResult Index()
        {
            GetKactusEmpleadoAsync();

            //*******************************************************
            //EnviarMensaje();
            //*****************************************************
            List<InfoLegalizacion> model = new List<InfoLegalizacion>();
            EngineDb Metodo = new EngineDb();

            string usuarioCargo = HttpContext.Session.GetString("Usuario_Cargo");
            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");

            //model = Metodo.SolicitudesAntPendientesLegalizacion("Sp_GetSolicitudesAnticiposPendientesLegalizacion",
            //    string.Empty);

            if (usuarioCargo == "3")
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
            Metodo.InsertKactusEmpleados("", empleado);
            return empleado;
        }

       /* private void EnviarMensaje()
        {
            List<string> listaDestino = new List<string>();
            //listaDestino.Add("d.sanchez@innova4j.com");
            //listaDestino.Add("efrainmejiasc@gmail.com");
            listaDestino.Add("e.mejias@innova4j.com");
            //listaDestino.Add("ha.development.org@gmail.com");
            //listaDestino.Add("abetancourt@innova4j.com");

            Email model = new Email
            {
                Fecha = DateTime.Now.ToString("dd/MM/yyyy"),
                NombreDestinatario = "Angelica Betancourt",
                NumeroDocumento ="0005896",
                Direccion = "Medellin - Antioquia - Colombia, Tlf : +57 031 3458902 "
            };
            //****************************************************************************************************
            string body = System.IO.Path.Combine(env.WebRootPath, "EmailTemplate", "TemplateEmail.cshtml");
            EngineMailSend Enviar = new EngineMailSend("Prueba Notificacion STF", body, string.Empty, listaDestino,model);
            bool resultado = Enviar.EnviarMail();
            //*****************************************************************************************************
            string msjGet = string.Empty;
            if (resultado)
                msjGet = "Notificacion enviada satisfactoriamente";
            else
                msjGet = Enviar.ErrorEnviando();
        }*/

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear(int id)
        {
            // Obtenemos datos del empleado en Session

            string cedula = "";
            string cargo = "";
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cedula")) && !string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cedula = HttpContext.Session.GetString("Usuario_Cedula");
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            if (cargo == "2")
                ViewBag.MostrarTasa = true;
            else
                ViewBag.MostrarTasa = false;

            var ListaBanco = bancoRepository.All().ToList();
            //var Obanco = new Banco
            //{
            //    Id = 0,
            //    Nombre = "Sin definir"
            //};
            //ListaBanco.Add(Obanco);

            var ListaMoneda = monedaRepository.All().ToList();

            if (id == 0) //si viene con el valor "0" se refiere a una legalizacion sin anticipo. Solo debo de cargar los bancos y la moneda
            {
                var ListaCentroCosto = new List<CentroCosto>();
                var ListaCentroOperaciones = new List<CentroOperacion>();
                var ListaUnidadNegocio = new List<UnidadNegocio>();

                var ListaEmpleado = objUNOEE.EmpleadoAll();
                var OLegalizaciones = new LegalizacionesViewModel
                {
                    DocumentoERPID = 11111,
                    ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                    ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                    ListaEmpleado = new SelectList(ListaEmpleado, "Cedula", "Nombre"),
                    ConAnticipo = 0,
                    CedulaId = cedula,
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
                    ReciboCaja = 0,
                    Consignacion = 0,
                    Valor = "0",
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

                //Se valida que exista un flujo para la solicitud
                //Se obtiene el paso inicial del flujo configurado
                //if (!getPasoInicialFlujo(OLegalizacionHeader, solicitud.DestinoID, (float)solicitud.Monto))
                //{
                //    TempData["Alerta"] = "warning - No hay un flujo de aprobación creado para esta solicitud. Comuníquese con el administrador de sistema.";
                //    return View("Crear", solicitud);
                //}

                //legalizacionRepository.Insert(OLegalizacionHeader);


                //Se actualiza el estado de la solicitud a Legalizada
                if (OLegalizacionHeader.Id > 0)
                {
                    if(legalizacion.AnticipoId > 0)
                    {
                        var solicitud = solicitudRepository.Find(legalizacion.AnticipoId);
                        solicitud.EstadoId = 2; //Legalizada
                        solicitudRepository.Update(solicitud);
                    }
                }

                //Aplico Formato JSON a los thead que vienen de la tabla
                legalizacion.GastosJSON = TableToJSON(legalizacion.GastosJSON);

                //creo la lista de los detalles que vienen del json recorro la lista y guardo el detalle en la bd
                var LegalizacionGastos = JsonConvert.DeserializeObject<List<DecerializeLegalizacionGasto>>(legalizacion.GastosJSON);
                foreach (var item in LegalizacionGastos)
                {
                    var wOLegalizacionGasto = new LegalizacionGastos
                    {
                        FechaGasto = item.FechaGasto,
                        LegalizacionId = OLegalizacionHeader.Id,
                        CentroOperacionId = 1,
                        UnidadNegocioId = 1,
                        CentroCostosId = 1,
                        MotivoId = 1,
                        PaisId = item.PaisId,
                        CiudadId = item.CiudadId,
                        TipoServicioId = item.TipoServicioId,
                        ProveedorId = item.ProveedorId

                    };
                    //legalizacionGastosRepository.Insert(wOLegalizacionGasto);
                }


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

                var LegalizacionGastos = JsonConvert.DeserializeObject<List<DecerializeLegalizacionGasto>>(legalizacion.GastosJSON);
                foreach (var item in LegalizacionGastos)
                {
                    var wOLegalizacionGasto = new LegalizacionGastos
                    {
                        FechaGasto = item.FechaGasto,
                        LegalizacionId = OLegalizacionHeader.Id,
                        CentroOperacionId = 1,
                        UnidadNegocioId = 1,
                        CentroCostosId = 1,
                        MotivoId = 1,
                        PaisId = item.PaisId,
                        CiudadId = item.CiudadId,
                        TipoServicioId = item.TipoServicioId,
                        ProveedorId = item.ProveedorId

                    };
                    legalizacionGastosRepository.Insert(wOLegalizacionGasto);

                }

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
        [Route("LegalizacionDetalles")]
        public ActionResult Ver(int Id)
        {
            Legalizacion legalizacion = legalizacionRepository.Find(Id);
            legalizacion.Solicitud = solicitudRepository.Find(legalizacion.SolicitudID);
            legalizacion.SolicitudGastos = solicitudGastosRepository.All().Where(a=> a.SolicitudId == legalizacion.SolicitudID).ToList();
            legalizacion.LegalizacionGastos =
                legalizacionGastosRepository.All().Where(a => a.LegalizacionId == Id).ToList();
            legalizacion.Empleado = objUNOEE.getEmpleadoCedula(legalizacion.Solicitud.EmpleadoCedula);

            foreach (var item in legalizacion.LegalizacionGastos)
            {
                item.Pais = paisRepository.Find(item.PaisId);
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

            @ViewBag.SumLega = legalizacion.LegalizacionGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Valor));
            @ViewBag.SumSol  = legalizacion.SolicitudGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Monto));

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
            var legalizacion = legalizacionRepository.Find(id);
            legalizacion.Solicitud = solicitudRepository.Find(legalizacion.SolicitudID);
            legalizacion.SolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == legalizacion.SolicitudID).ToList();
            legalizacion.LegalizacionGastos =
                legalizacionGastosRepository.All().Where(a => a.LegalizacionId == id).ToList();
            legalizacion.Empleado = objUNOEE.getEmpleadoCedula(legalizacion.Solicitud.EmpleadoCedula);

            foreach (var item in legalizacion.LegalizacionGastos)
            {
                item.Pais = paisRepository.Find(item.PaisId);
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

            @ViewBag.SumLega = legalizacion.LegalizacionGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Valor));
            @ViewBag.SumSol = legalizacion.SolicitudGastos.AsEnumerable().Sum(o => Convert.ToDecimal(o.Monto));

            return View(legalizacion);
        }


        private bool getPasoInicialFlujo(Legalizacion legalizacion, int? destino, float monto)
        {
            //Obtengo el Id del flujo
            var idFlujo = flujoSolicitudRepository.All().Where(m => m.DestinoId == destino && monto >= m.MontoMinimo && monto <= m.MontoMaximo).Select(m => m.Id).LastOrDefault();

            if (idFlujo != null && idFlujo > 0)
            {
                var paso = pasoFlujoSolicitudRepository.All().Where(m => m.FlujoSolicitudId == idFlujo && m.Estatus == 1).OrderBy(m => m.Orden).Select(m => m.Id).FirstOrDefault();

                legalizacion.FlujoSolicitudId = idFlujo;
                legalizacion.PasoFlujoSolicitudId = paso;
                return true;
            }
            else
            {
                return false;
            }
        }

        private string TableToJSON(string json)
        {
            json = json.Replace("Centro Operación", "CentroOperacion")
                        .Replace("Unidad Negocio", "UnidadNegocio")
                        .Replace("Centro Costo", "CentroCosto")
                        .Replace("Fecha Gasto", "FechaGasto");
            return json;
        }


    }
}