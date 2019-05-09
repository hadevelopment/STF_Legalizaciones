using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Interface;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Interface.ISolicitud;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class LocalidadController : Controller
    {
        private readonly IDestinoRepository destinoRepository;
        private readonly IZonaRepository zonaRepository;
        private readonly ICiudadRepository estadoRepository;
        private readonly IPaisRepository paisRepository;
        private readonly ICiudadRepository ciudadRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly ISolicitudRepository SolicitudRepository;

        public LocalidadController(IDestinoRepository destinoRepository, IZonaRepository zonaRepository, ICiudadRepository estadoRepository, IPaisRepository paisRepository, ICiudadRepository ciudadRepository, IMonedaRepository monedaRepository, ISolicitudRepository SolicitudRepository) {
            this.destinoRepository = destinoRepository;
            this.zonaRepository = zonaRepository;
            this.estadoRepository = estadoRepository;
            this.paisRepository = paisRepository;
            this.ciudadRepository= ciudadRepository;
            this.monedaRepository = monedaRepository;
            this.SolicitudRepository = SolicitudRepository;
        }

     
        public IActionResult Index()
        {
            return View();
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

        public JsonResult DestinosEdit(int Id)
        {
            var OSolicitud = SolicitudRepository.Find(Id);
            var ListDestinos = destinoRepository.All().ToList();

            foreach (var item in ListDestinos)
            {
                if (OSolicitud.DestinoID == item.Id)
                    item.Nombre = item.Nombre + "XX";

            }


            return Json(ListDestinos);
        }

        public JsonResult ZonasDestinosMaeEdit(int Id)
        {
            var ListZonaDestino = destinoRepository.All().ToList();
            foreach (var item in ListZonaDestino)
            {
                if (item.Id == Id)
                    item.Nombre = item.Nombre + "XX";

            }
            return Json(ListZonaDestino);
        }

        public JsonResult ZonasDestinosEdit(int Id)
        {
            var OSolicitud = SolicitudRepository.Find(Id);
            var ListZonaDestino = zonaRepository.All().Where(x => x.DestinoID == OSolicitud.DestinoID).ToList();
            foreach (var item in ListZonaDestino)
            {
                if (OSolicitud.ZonaID == item.Id)
                    item.Nombre = item.Nombre + "XX";

            }
            return Json(ListZonaDestino);
        }
        public JsonResult MonedasEdit(int Id)
        {
            var OSolicitud = SolicitudRepository.Find(Id);
            var ListMoneda = monedaRepository.All().ToList();
            foreach (var item in ListMoneda)
            {
                if (OSolicitud.MonedaId == item.Id)
                    item.Nombre = item.Nombre + "XX";

            }
            return Json(ListMoneda);
        }

    }
}