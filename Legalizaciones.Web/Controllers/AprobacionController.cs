using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Model.Workflow;
using Legalizaciones.Web.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class AprobacionController : Controller
    {
        enum TipoAccion
        {
            Aprobar = 1,
            Rechazar = 2
        }

        /// <summary>
        /// Metodo principal que carga las solicitudes [Anticipo/Legalizacion] pendientes por aprobar del usuario
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            Aprobacion aprobacion = new Aprobacion();
            EngineDb DB = new EngineDb();

            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            if (!string.IsNullOrEmpty(usuarioCedula))
            {
                aprobacion = DB.ObtenerSolicitudesPorAprobar(usuarioCedula);
            }

            return View(aprobacion);
        }

        /// <summary>
        /// Metodo para aprobar una solicitud de Anticipo
        /// </summary>
        /// <param name="Id">identificador del anticipo</param>
        /// <returns></returns>
        public IActionResult AprobarAnticipo(int Id)
        {
            EngineDb DB = new EngineDb();

            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            string usuarioNombre = HttpContext.Session.GetString("Usuario_Nombre");
            if (!string.IsNullOrEmpty(usuarioCedula) && !string.IsNullOrEmpty(usuarioNombre))
            {
                string aprobador = usuarioCedula + " - " + usuarioNombre;
                bool result = DB.GestionAnticipo(Id, (int)TipoAccion.Aprobar, aprobador, "Aprobación de Anticipo");

                if (result)
                {
                    TempData["Alerta"] = "success - El anticipo ha sido aprobado exitosamente.";
                }
                else
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de aprobar el anticipo.";
                }
            }

            return RedirectToAction("Index", "Aprobacion");
        }


        /// <summary>
        /// Metodo para Rechazar una solicitud de Anticipo
        /// </summary>
        /// <param name="Id">identificador del anticipo</param>
        /// <returns></returns>
        [Route("/Aprobacion/RechazarAnticipo/{Id}/{Motivo}")]
        public IActionResult RechazarAnticipo(int Id, string Motivo)
        {
            EngineDb DB = new EngineDb();

            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            string usuarioNombre = HttpContext.Session.GetString("Usuario_Nombre");
            if (!string.IsNullOrEmpty(usuarioCedula) && !string.IsNullOrEmpty(usuarioNombre))
            {
                string aprobador = usuarioCedula + " - " + usuarioNombre;
                bool result = DB.GestionAnticipo(Id, (int)TipoAccion.Rechazar, aprobador, Motivo);

                if (result)
                {
                    TempData["Alerta"] = "success - El anticipo ha sido rechazado exitosamente.";
                }
                else
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de aprobar el anticipo.";
                }
            }

            return RedirectToAction("Index", "Aprobacion");
        }
    }
}