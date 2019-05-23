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
        public IActionResult Index(string tipoDocumento = "",string addAprobador = "", string addMail = "" ,string empleado = "",string descripcion = "S/D",int paso = 0 ,string clear = "")
        {
            AprobacionDocumento model = new AprobacionDocumento();
            model = GetTipoSolicitudes(model);
            ViewBag.Paso = 0;
            if((tipoDocumento != "Seleccione..." && tipoDocumento != string.Empty && tipoDocumento != null) && addAprobador != string.Empty && addMail != string.Empty && empleado != string.Empty)
            {
                int estatus = 1;
                int update = 0;
                EngineStf Funcion = new EngineStf();
                if (paso == 0) {
                    update = 1;
                    paso++;
                }
                else { 
                    update = 0;
                }
                model = Funcion.SetCreateAprobador(model,tipoDocumento,addAprobador,empleado,descripcion,addMail,update,estatus,paso);
                ViewBag.Paso = model.FlujoAprobacion.Count + 1;
                ViewBag.TipoDocumento = tipoDocumento;

            }
            else if(clear != string.Empty)
            {
                ViewBag.Paso = 0;
                ViewBag.TipoDocumento = null;
                model.FlujoAprobacion = null;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(int n = 0)
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

        public  IActionResult UpdateFlujo (string subProceso ,int id , int orden , string descripcion, string nombreAprobador , string emailAprobador)
        {
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = "1000";
            EngineStf Funcion = new EngineStf();
            ViewBag.UpdateFlujo = "UPDATE FLUJO";
            return RedirectToAction("Index", "WorkFlow",model);
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