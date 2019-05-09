using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Engine;
using Legalizaciones.Web.Models;

namespace Legalizaciones.Web.Controllers
{
    public class LegalizacionesController : Controller
    {
        public IActionResult Index()
        {
            List<InfoLegalizacion> model = new List<InfoLegalizacion>();
            EngineDb Metodo = new EngineDb();
            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            model = Metodo.SolicitudesAntPendientesLegalizacion("Sp_GetSolicitudesAnticiposPendientesLegalizacion", usuarioCedula); ;
            return View(model);
        }

        public ActionResult  Editar (int idSolicitud = 0)
        {
            return RedirectToAction("Index", "Legalizaciones");
        }

        public ActionResult Ver (int idSolicitud = 0)
        {
            return RedirectToAction("Index", "Legalizaciones");
        }

        public ActionResult Legalizar(int idSolicitud = 0)
        {
            return RedirectToAction("Index", "Legalizaciones");
        }
    }
}