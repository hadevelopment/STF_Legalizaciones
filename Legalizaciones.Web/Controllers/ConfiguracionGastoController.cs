using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Interface;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class ConfiguracionGastoController : Controller
    {
        private readonly IDestinoRepository destinoRepository;
        private readonly IZonaRepository zonaRepository;
        private readonly ICiudadRepository estadoRepository;
        private readonly IPaisRepository paisRepository;
        private readonly ICiudadRepository ciudadRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly IConfiguracionGastoRepository configuracionGastoRepository;

        public ConfiguracionGastoController(IDestinoRepository destinoRepository, IZonaRepository zonaRepository, ICiudadRepository estadoRepository, IPaisRepository paisRepository, ICiudadRepository ciudadRepository, IMonedaRepository monedaRepository, IConfiguracionGastoRepository configuracionGastoRepository) {
            this.destinoRepository = destinoRepository;
            this.zonaRepository = zonaRepository;
            this.estadoRepository = estadoRepository;
            this.paisRepository = paisRepository;
            this.ciudadRepository= ciudadRepository;
            this.monedaRepository = monedaRepository;
            this.configuracionGastoRepository = configuracionGastoRepository;
        }

     
        public IActionResult Index()
        {
            var ConfiguracionGastos = configuracionGastoRepository.All().Where(a =>a.Estatus == 1).ToList();
            return View(ConfiguracionGastos);
        }

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear()
        {
            return View(new ConfiguracionGasto());
        }

        [HttpGet]
        [Route("Editar")]
        public ActionResult Editar(int id)
        {
            var conf = configuracionGastoRepository.Find(id);
            return View(conf);
        }

        [HttpGet]
        [Route("DetallesConfiguracion")]
        public ActionResult Ver(int id)
        {
            var conf = configuracionGastoRepository.Find(id);
            return View(conf);
        }

        [HttpPost]
        [Route("ActualizarConfiguracion")]
        public ActionResult Actualizar(ConfiguracionGasto configuracionGasto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    configuracionGasto.Estatus = 1;
                    configuracionGastoRepository.Update(configuracionGasto);

                    TempData["Alerta"] = "success - La Configuración se registro correctamente.";
                    
                }
                catch (System.Exception e)
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la configuración";
                }
            }

            return RedirectToAction("Index", "ConfiguracionGasto");
        }

        [HttpPost]
        [Route("Crear")]
        public ActionResult Guardar(ConfiguracionGasto configuracionGasto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    configuracionGasto.Estatus = 1;
                    configuracionGasto.FechaCreacion = DateTime.Today;
                    configuracionGastoRepository.Insert(configuracionGasto);

                    TempData["Alerta"] = "success - La Configuración se registro correctamente.";
                    return RedirectToAction("Index", "ConfiguracionGasto");
                }
                catch (System.Exception e)
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la configuración";
                }
            }
           
            return View("Crear", configuracionGasto);
        }

        [HttpPost]
        [Route("Eliminar")]
        public ActionResult Eliminar(ConfiguracionGasto configuracionGasto)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    configuracionGasto.Estatus = 0;
                    configuracionGastoRepository.Update(configuracionGasto);

                    TempData["Alerta"] = "success - La Configuración se elimino correctamente.";

                }
                catch (System.Exception e)
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de registrar la configuración";
                }
            }

            return RedirectToAction("Index", "ConfiguracionGasto");
        }

        public JsonResult Destinos()
        {
            return Json(destinoRepository.All());
        }

        public JsonResult ZonasDestinos(int  ? destinoID)
        {
            if (destinoID == null)
            {
               return Json(zonaRepository.All());  //Obtiene todas las zonas cuando no pasan parametros o es NULL
            }
            else
            { 
                return Json(zonaRepository.All().Where(x=> x.DestinoID == destinoID));
            }
        }

        public JsonResult Monedas()
        {
            return Json(monedaRepository.All());
        }

        public JsonResult Paises()
        {
                return Json(paisRepository.All());
        }
        public JsonResult CiudadesPais(int paisID)
        {

            return Json(ciudadRepository.All().Where(x => x.PaisID == paisID));
        }

        public JsonResult obtenerLimiteGasto(int paisID, string tipoServicioID, int monedaID)
        {
            string cargo = "";
            // Obtenemos datos del empleado en Session
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            var result = configuracionGastoRepository.All()
                .Where(
                    m => m.CargoId == cargo &&
                    m.PaisId == paisID &&
                    m.TipoServicioId == tipoServicioID &&
                    m.MonedaId == monedaID).LastOrDefault();

            return Json(result);
        }

        public JsonResult obtenerLimiteGastoRuta(int paisID, string tipoServicioID, int monedaID, int origenID, int destinoID )
        {
            string cargo = "";
            // Obtenemos datos del empleado en Session
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("Usuario_Cargo")))
            {
                cargo = HttpContext.Session.GetString("Usuario_Cargo");
            }

            var result = configuracionGastoRepository.All()
                .Where(
                    m => m.CargoId == cargo &&
                    m.PaisId == paisID &&
                    m.TipoServicioId == tipoServicioID &&
                    m.MonedaId == monedaID &&
                    m.ZonaOrigenId == origenID &&
                    m.ZonaDestinoId == destinoID).LastOrDefault();


            return Json(result);
        }
    }
}