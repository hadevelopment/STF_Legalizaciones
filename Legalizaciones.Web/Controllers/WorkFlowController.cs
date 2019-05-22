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
            AprobacionDocumento model = new AprobacionDocumento();
            model = GetTipoSolicitudes(model);
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int n = 0)
        {
            string tipoSolicitud = Request.Form["tipoSolicitud"];
            string data = Request.Form.ToString();
            AprobacionDocumento model = new AprobacionDocumento();
            model = GetTipoSolicitudes(model);
            if (tipoSolicitud != "Seleccione...")
            {
                model.Aprobadores = GetAprobadores(tipoSolicitud);
                return (Json(model.Aprobadores));
            }
            return View(model);
        }

        private AprobacionDocumento  GetTipoSolicitudes(AprobacionDocumento model)
        {
            Engine.EngineDb Metodo = new EngineDb();
            model.TipoSolicitud = Metodo.TiposDocumentos("Sp_GetTiposSolicitud");
            return model;
        }


        public List<DataAprobacion>  GetAprobadores(string tipoSolicitud = "")
        {
            Engine.EngineDb Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            model = Metodo.AprobadoresTipoSolicitud("Sp_GetFlujoAprobadoresSolicitud", tipoSolicitud);
            return model;
        }
    }
}