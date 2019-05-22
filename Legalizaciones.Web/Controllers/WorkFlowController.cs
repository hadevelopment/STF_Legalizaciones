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
        public IActionResult Index(string tipoDocumento = "", string addAprobador = "", string empleado = "", string descripcion = "S/D", string mailApr = "", int paso = 0 )
        {
            AprobacionDocumento model = new AprobacionDocumento();
            model = GetTipoSolicitudes(model);
            ViewBag.Paso = 0;
            if((tipoDocumento != "Seleccione..." && tipoDocumento != string.Empty ) && addAprobador != string.Empty && empleado != string.Empty)
            {
                int estatus = 1;
                int update = 0;
                EngineStf Funcion = new EngineStf();
                if (paso == 1)
                    update = paso;
                else
                    update = 0;
                model = Funcion.SetCreateAprobador( model , tipoDocumento , addAprobador ,  empleado , descripcion, mailApr,update,estatus,paso);
                ViewBag.Paso = model.FlujoAprobacion.Count + 1;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(int n = 0)
        {
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = Request.Form["tipoSolicitud"];
            model = GetTipoSolicitudes(model);
            if (model.TipoSeleccionado != "Seleccione...")
            {
              model.Aprobadores = GetAprobadores(model.TipoSeleccionado);
                if (model.Aprobadores.Count == 0)
                    model.Aprobadores = null;

            }
            return View(model);
        }

        private AprobacionDocumento  GetTipoSolicitudes(AprobacionDocumento model)
        {
            Engine.EngineDb Metodo = new EngineDb();
            model.TipoSolicitud = Metodo.TiposDocumentos("Sp_GetTiposSolicitud");
            return model;
        }


        public List<DataAprobacion> GetAprobadores(string tipoSolicitud = "")
        {
            Engine.EngineDb Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            model = Metodo.AprobadoresTipoSolicitud("Sp_GetFlujoAprobadoresSolicitud", tipoSolicitud);
            return model;
        }
    }
}