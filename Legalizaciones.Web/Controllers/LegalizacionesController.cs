using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class LegalizacionesController : Controller
    {
        public IActionResult Index()
        {
          
            string usuarioCedula = HttpContext.Session.GetString("Usuario_Cedula");
            return View();
        }
    }
}