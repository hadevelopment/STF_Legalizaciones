using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Engine;
using Legalizaciones.Erp.Serializer;
using Legalizaciones.Erp.Models;
using Legalizaciones.Erp;
using Newtonsoft.Json;

namespace Legalizaciones.Web.Controllers
{
    public class ErpController : Controller
    {
        public IActionResult Index()
        {
            ErpMethod Servicio = new ErpMethod();
            // var UnidadNegocio = Servicio.UnidadNegocioSingleAsync("01");
            // var UnidadesNegocios = Servicio.UnidadNegocioCollectionAsync();
            var centroOperacion = Servicio.CentroOperacionesSingleAsync("27");
           // var centroOperacion = Servicio.CentroOperacionesCollectionAsync();
            return View();
        }
    }
}