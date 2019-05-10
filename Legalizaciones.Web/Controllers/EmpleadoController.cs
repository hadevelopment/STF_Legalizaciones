using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Interface;
using Legalizaciones.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository empleadoRepository;
        private readonly IEmpleadoPermisoRepository empleadoPermisoRepository;

        public EmpleadoController(IEmpleadoRepository _empleadoRepository, IEmpleadoPermisoRepository _empleadoPermisoRepository)
        {
            this.empleadoRepository = _empleadoRepository;
            this.empleadoPermisoRepository = _empleadoPermisoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PreferenciasLegalizacion(string cedula = "")
        {
            return View(empleadoPermisoRepository.All().Where(m => m.EmpleadoCedula == cedula).ToList());
        }

        [HttpPost]
        public IActionResult PreferenciasLegalizacion(Empleado empleado)
        {
            EmpleadoPermiso empleadoPermiso = new EmpleadoPermiso();
            empleadoPermiso.EmpleadoCedula = HttpContext.Session.GetString("Usuario_Cedula");
            empleadoPermiso.EmpleadoPermisoCedula = empleado.Cedula;
            empleadoPermiso.EmpleadoPermisoNombre = String.Concat(empleado.Nombre, empleado.Apellido);
            empleadoPermiso.TipoPermisoId = 3;

            empleadoPermisoRepository.Insert(empleadoPermiso);

            TempData["Alerta"] = "success - La Solicitud se registro correctamente.";

            return RedirectToAction("PreferenciasLegalizacion", new { cedula = HttpContext.Session.GetString("Usuario_Cedula") });
        }

        [HttpDelete]
        public IActionResult PreferenciasLegalizacionEliminar(int id)
        {
            var empleado = empleadoPermisoRepository.Find(id);
            try
            {
                empleadoPermisoRepository.Delete(empleadoPermisoRepository.Find(id));
                TempData["Alerta"] = "success - La Solicitud se registro correctamente.";
            }
            catch (Exception)
            {
                TempData["Alerta"] = "error - La Solicitud se registro correctamente.";
                throw;
            }


            return View("PreferenciasLegalizacion", new { cedula = HttpContext.Session.GetString("Usuario_Cedula") });
        }
    }
}