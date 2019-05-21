using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Models;
using Legalizaciones.Web.Engine;

namespace Legalizaciones.Web.Controllers
{
    public class WorkFlowController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.NoExisteFlujo = null;
            AprobacionDocumento model = new AprobacionDocumento();
            model = GetTipoSolicitudes(model);
            return View(model);
        }

        private AprobacionDocumento  GetTipoSolicitudes(AprobacionDocumento model)
        {
            Engine.EngineDb Metodo = new EngineDb();
            model.TipoSolicitud = Metodo.TiposDocumentos("Sp_GetTiposSolicitud");
            return model;
        }

        [HttpGet]
        public  JsonResult GetAprobadores(string tipoSolicitud)
        {
            Engine.EngineDb Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            model = Metodo.AprobadoresTipoSolicitud("Sp_GetFlujoAprobadoresSolicitud", tipoSolicitud);
            return Json(model);
        }
    }
}