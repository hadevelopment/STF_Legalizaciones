using System.Collections.Generic;
using System.Linq;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Mvc;


namespace Legalizaciones.Web.Controllers
{
    [Route("Zona")]
    public class ZonaController : Controller
    {
        private readonly IZonaRepository zonaRepository;
        private readonly IDestinoRepository destinoRepository;

        public ZonaController(
            IZonaRepository zonaRepository,
            IDestinoRepository destinoRepository
        )
        {
            this.zonaRepository    = zonaRepository;
            this.destinoRepository = destinoRepository;
        }

        public IActionResult Index()
        {
            List<Zona> Zonas = zonaRepository.All().Where(x => x.Estatus == 1).ToList();
            foreach (var zona in Zonas)
            {
                zona.Destino = destinoRepository.Find(long.Parse(zona.DestinoID.ToString()));
            }
            return View(Zonas);
        }

        /*     ****************************************************************************
       *     Descripcion: VISTA para acceder a datos de la Solicitud. 
       *     Creada: 05-06-2019 
       *     Autor: Javier Rodriguez    
             **************************************************************************** */

        [HttpPost]
        [Route("Guardar")]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar([Bind] Zona zona)
        {
            if (ModelState.IsValid)
            {
                zona.Estatus = 1;
                if (zona.Id == 0)
                {
                    zonaRepository.Insert(zona);
                }
                else
                {
                    zonaRepository.Update(zona);
                }
            }

            return RedirectToAction("Index", "Zona");
        }


        /*     ****************************************************************************
        *     Descripcion: VISTA para acceder a datos de la Solicitud. 
        *     Creada: 05-06-2019 
        *     Autor: Javier Rodriguez    
              **************************************************************************** */

        [HttpGet]
        [Route("Visualizar")]
        public ActionResult Visualizar(int id)
        {
            var zona = zonaRepository.Find(id);
            zona.Destino = destinoRepository.Find(long.Parse(zona.DestinoID.ToString()));

            return View(zona);
        }


        /*    ****************************************************************************
*     Descripcion: VISTA para acceder a datos de la Solicitud. 
*     Creada: 05-07-2019 
*     Autor: Javier Rodriguez    
     **************************************************************************** */

        [HttpGet]
        [Route("Crear")]
        public ActionResult Crear()
        {
            var zona = new Zona();
            zona.Id = 0;

            return View(zona);
        }


        /*     ****************************************************************************
       *     Descripcion: VISTA para acceder a datos de la Solicitud. 
       *     Creada: 05-06-2019 
       *     Autor: Javier Rodriguez    
             **************************************************************************** */

        [HttpGet]
        [Route("Editar")]
        public ActionResult Editar(int id)
        {
            var zona = zonaRepository.Find(id);
            zona.Destino = destinoRepository.Find(long.Parse(zona.DestinoID.ToString()));

            return View(zona);
        }


        /*     ****************************************************************************
      *     Descripcion: VISTA para eliminar logicamente. 
      *     Creada: 05-06-2019 
      *     Autor: Javier Rodriguez    
            **************************************************************************** */

        [HttpPost]
        [Route("Eliminar")]
        public ActionResult Eliminar([Bind] Zona zona)
        {
            zona.Estatus = 0;
            zonaRepository.Update(zona);

            return RedirectToAction("Index", "Zona");
        }

    }
}