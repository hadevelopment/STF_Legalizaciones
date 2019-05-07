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
        public IActionResult Index()
        {
            return View();
        }
    }
}