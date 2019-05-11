using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Engine;
using Legalizaciones.Web.Models;
using Legalizaciones.Model;
using Legalizaciones.Interface;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Web.Helpers;
using Legalizaciones.Web.Models.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace Legalizaciones.Web.Controllers
{
    [Route("Legalizaciones")]
    public class LegalizacionesController : Controller
    {
        private readonly ISolicitudRepository solicitudRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private readonly IBancoRepository bancoRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly ILegalizacionRepository legalizacionRepository;
        private readonly ILegalizacionGastosRepository legalizacionGastosRepository;

        public UNOEE objUNOEE = new UNOEE();

        public LegalizacionesController(ISolicitudRepository solicitudRepository, ISolicitudGastosRepository solicitudGastosRepository, IBancoRepository bancoRepository, IMonedaRepository monedaRepository, ILegalizacionRepository legalizacionRepository, ILegalizacionGastosRepository legalizacionGastosRepository)
        {
            this.solicitudRepository = solicitudRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.bancoRepository = bancoRepository;
            this.monedaRepository = monedaRepository;
            this.legalizacionRepository = legalizacionRepository;
            this.legalizacionGastosRepository = legalizacionGastosRepository;
        }
        public IActionResult Index()
        {
            List<InfoLegalizacion> model = new List<InfoLegalizacion>();
            EngineDb Metodo = new EngineDb();

            string usuarioCargo = HttpContext.Session.GetString("Usuario_Cargo");
            string usuarioCedula = string.Empty;
            if (usuarioCargo == "3")
            {
                model = Metodo.SolicitudesAntPendientesLegalizacion("Sp_GetSolicitudesAnticiposPendientesLegalizacion", string.Empty); 
            }
            else
            {
                try
                {
                    usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
                }
                catch
                {
                    usuarioCedula = string.Empty;
                }

                if (usuarioCedula != string.Empty)
                    model = Metodo.SolicitudesAntPendientesLegalizacion("Sp_GetSolicitudesAnticiposPendientesLegalizacion", usuarioCedula);
                else
                    return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear(int id)
        {

            var Osolicitud = solicitudRepository.Find(id);
            var ListsolicitudGastos = solicitudGastosRepository.All().Where(a => a.SolicitudId == id).ToList();
            var OEmpleado = objUNOEE.getEmpleadoCedula(Osolicitud.EmpleadoCedula);
            var OCentroCosto = objUNOEE.getCentroCosto(Osolicitud.CentroCostoId);
            var OCentroOperaciones = objUNOEE.getCentroOperacion(Osolicitud.CentroOperacionId);
            var OUnidadNegocio = objUNOEE.getUnidadNegocio(Osolicitud.UnidadNegocioId);

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

            var ListaBanco = bancoRepository.All().ToList();
            var ListaMoneda = monedaRepository.All().ToList();
            var ListaMotivo = objUNOEE.GetListMotivos(Osolicitud.CentroCostoId);

            var OLegalizaciones = new LegalizacionesViewModel
            {
                AnticipoId = Osolicitud.Id,
                DocumentoERPID = 0,
                FechaRegistro = Osolicitud.FechaSolicitud,
                FechaVencimiento = Osolicitud.FechaVencimiento,
                Concepto = Osolicitud.Concepto,
                Monto = Osolicitud.Monto,
                CentroCosto = OCentroCosto.Nombre,
                CentroOperacion = OCentroOperaciones.Nombre,
                UnidadNegocio = OUnidadNegocio.Nombre,
                FechaDesde = Osolicitud.FechaDesde,
                FechaHasta = Osolicitud.FechaHasta,
                Nombre = OEmpleado.Nombre,
                Cedula = OEmpleado.Cedula,
                Cargo = wCargo,
                Area = OEmpleado.Area,
                ListaBanco = new SelectList(ListaBanco, "Id", "Nombre"),
                ListaMoneda = new SelectList(ListaMoneda, "Id", "Nombre"),
                MonedaId = Osolicitud.MonedaId,
                ListaMotivo = new SelectList(ListaMotivo, "Id", "Nombre"),
                SolicitudGastos = ListsolicitudGastos

            };

            return View(OLegalizaciones);
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
                    BancoId = legalizacion.BancoId
                };
                legalizacionRepository.Insert(OLegalizacionHeader);


                //Se actualiza el estado de la solicitud a Legalizada
                if(OLegalizacionHeader.Id != null && OLegalizacionHeader.Id > 0)
                {
                    var solicitud = solicitudRepository.Find(legalizacion.AnticipoId);
                    solicitud.EstadoId = 2;//Legalizada
                    solicitudRepository.Update(solicitud);
                }

                //creo la lista de los detalles que vienen del json recorro la lista y guardo el detalle en la bd
                var LegalizacionGastos = JsonConvert.DeserializeObject<List<LegalizacionGastos>>(legalizacion.GastosJSON);
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
    }
}