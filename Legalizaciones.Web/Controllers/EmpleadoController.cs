using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoRepository empleadoRepository;

        public EmpleadoController(IEmpleadoRepository _empleadoRepository)
        {
            this.empleadoRepository = _empleadoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Empleados()
        {
            return Json(empleadoRepository.All());
        }
    }
}