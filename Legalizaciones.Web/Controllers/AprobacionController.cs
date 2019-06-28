using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Interface;
using Legalizaciones.Model.Workflow;
using Legalizaciones.Web.Engine;
using Legalizaciones.Web.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class AprobacionController : Controller
    {
        public UNOEE erp = new UNOEE();
        private readonly IKactusEmpleadoRepository kactusEmpleadoRepository;

        enum TipoAccion
        {
            Aprobar = 1,
            Rechazar = 2
        }

        public AprobacionController(IKactusEmpleadoRepository kactusEmpleadoRepository)
        {
            this.kactusEmpleadoRepository = kactusEmpleadoRepository;
        }

        /// <summary>
        /// Metodo principal que carga las solicitudes [Anticipo/Legalizacion] pendientes por aprobar del usuario
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            
            Aprobacion aprobacion = new Aprobacion();
            EngineDb DB = new EngineDb();

            string usuarioCargo = HttpContext.Session.GetString("Usuario_Cargo");
            if (!string.IsNullOrEmpty(usuarioCargo))
            {
                aprobacion = DB.ObtenerSolicitudesPorAprobar(usuarioCargo);
                foreach(var item in aprobacion.Anticipos)
                {
                    item.Empleado = kactusEmpleadoRepository.getEmpleadoCedula(item.EmpleadoCedula);
                }
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
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de rechazar el anticipo.";
                }
            }

            return RedirectToAction("Index", "Aprobacion");
        }



        /// <summary>
        /// Metodo para aprobar una solicitud de Anticipo
        /// </summary>
        /// <param name="Id">identificador de la Legalizacion</param>
        /// <returns></returns>
        public IActionResult AprobarLegalizacion(int Id)
        {
            EngineDb DB = new EngineDb();

            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            string usuarioNombre = HttpContext.Session.GetString("Usuario_Nombre");
            if (!string.IsNullOrEmpty(usuarioCedula) && !string.IsNullOrEmpty(usuarioNombre))
            {
                string aprobador = usuarioCedula + " - " + usuarioNombre;
                bool result = DB.GestionLegalizacion(Id, (int)TipoAccion.Aprobar, aprobador, "Aprobación de Legalizacion");

                if (result)
                {
                    TempData["Alerta"] = "success - La Legalización ha sido aprobada exitosamente.";
                }
                else
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de aprobar la Legalización.";
                }
            }

            return RedirectToAction("Index", "Aprobacion");
        }


        /// <summary>
        /// Metodo para Rechazar una solicitud de Anticipo
        /// </summary>
        /// <param name="Id">identificador del anticipo</param>
        /// <returns></returns>
        [Route("/Aprobacion/RechazarLegalizacion/{Id}/{Motivo}")]
        public IActionResult RechazarLegalizacion(int Id, string Motivo)
        {
            EngineDb DB = new EngineDb();

            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            string usuarioNombre = HttpContext.Session.GetString("Usuario_Nombre");
            if (!string.IsNullOrEmpty(usuarioCedula) && !string.IsNullOrEmpty(usuarioNombre))
            {
                string aprobador = usuarioCedula + " - " + usuarioNombre;
                bool result = DB.GestionLegalizacion(Id, (int)TipoAccion.Rechazar, aprobador, Motivo);

                if (result)
                {
                    TempData["Alerta"] = "success - La Legalización ha sido rechazada exitosamente.";
                }
                else
                {
                    TempData["Alerta"] = "error - Ocurrieron inconvenientes al momento de rechazar la Legalización.";
                }
            }

            return RedirectToAction("Index", "Aprobacion");
        }
    }
}