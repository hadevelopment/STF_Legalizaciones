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

        public ActionResult Aprobar(int idSolicitud = 0)
        {
            return RedirectToAction("Index", "Legalizaciones");
        }
    }
}