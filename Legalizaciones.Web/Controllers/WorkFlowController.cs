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
        public IActionResult Index(AprobacionDocumento model = null , string tipoDocumento = "",string addAprobador = "", string addMail = "" ,string empleado = "",string descripcion = "",int paso = 0 ,string clear = "")
        {
            if (model == null)
                model = new AprobacionDocumento();

            model = GetTipoSolicitudes(model);
            ViewBag.Paso = 0;
            if ((tipoDocumento != "Seleccione..." && tipoDocumento != string.Empty && tipoDocumento != null) && addAprobador != string.Empty && addMail != string.Empty && empleado != string.Empty)
            {
                int estatus = 1;
                int update = 0;
                EngineStf Funcion = new EngineStf();
                if (paso == 0)
                {
                    update = 1;
                    paso++;
                }
                else
                {
                    update = 0;
                }
                if (descripcion == string.Empty || descripcion == null)
                    descripcion = "S/D";
                model = Funcion.SetCreateAprobador(model, tipoDocumento, addAprobador, empleado, descripcion, addMail, update, estatus, paso);
                ViewBag.Paso = model.FlujoAprobacion.Count + 1;
                ViewBag.TipoDocumento = tipoDocumento;

            }
            else if (clear != string.Empty)
            {
                ViewBag.Paso = 0;
                ViewBag.TipoDocumento = null;
                model.FlujoAprobacion = null;
            }
            else if (model.TipoSeleccionado != "Seleccione..." && model.TipoSeleccionado != string.Empty && model.TipoSeleccionado != null)
            {
                model.Aprobadores = GetAprobadores(model.TipoSeleccionado);
                ViewBag.AprobadoresCount = model.Aprobadores.Count;
                if (model.Aprobadores.Count == 0)
                    model.Aprobadores = null;
            }
             if (model.CountDocAsociado > 0)
             {
                ViewBag.CountDocAsociado = model.CountDocAsociado;
             }
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string tipo = "")
        {
            AprobacionDocumento model = new AprobacionDocumento();
            if (tipo == string.Empty)
                model.TipoSeleccionado = Request.Form["tipoSolicitud"];
            else
                model.TipoSeleccionado = tipo;

            ViewBag.IndexSollicitud = Request.Form["indiceSolicitud"].ToString();
            model = GetTipoSolicitudes(model);
            if (model.TipoSeleccionado != "Seleccione...")
            {
              model.Aprobadores = GetAprobadores(model.TipoSeleccionado);
               ViewBag.AprobadoresCount = model.Aprobadores.Count;
                if (model.Aprobadores.Count == 0)
                    model.Aprobadores = null;

            }
            return View(model);
        }

        [HttpPost]
        public  IActionResult UpdateFlujo( int id = 0 ,int orden = 0 ,string descripcionT = "" , string nombre = "", string email ="" , string cedula ="", string type ="" ,string proceso = "")
        {
            EngineDb Metodo = new EngineDb();
            Metodo.UpdatePasoFlujoAprobacion("Sp_UpdatePasoAprobacion",id,descripcionT,cedula,nombre,email); 
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = type;
            return RedirectToAction("Index", "WorkFlow", model);
        }

        [HttpPost]
        public IActionResult EliminarFlujo(int ide = 0, string typee = "", string procesoe = "")
        {
            EngineDb Metodo = new EngineDb();
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = typee;
            model.CountDocAsociado = Metodo.CountDocAsociado("Sp_GetCountDocAsociadoPaso",ide,typee);
            if (model.CountDocAsociado > 0)
            {
                return RedirectToAction("Index", "WorkFlow", model);
            }
            Metodo.DeletePasoFlujoAprobacion("Sp_DeletePasoAprobacion", ide);
            return RedirectToAction("Index", "WorkFlow", model);
        }

        private AprobacionDocumento  GetTipoSolicitudes(AprobacionDocumento model)
        {
            Engine.EngineDb Metodo = new EngineDb();
            model.TipoSolicitud = Metodo.TiposDocumentos("Sp_GetTiposSolicitud");
            return model;
        }

        private List<DataAprobacion> GetAprobadores(string tipoSolicitud = "")
        {
            Engine.EngineDb Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            model = Metodo.AprobadoresTipoSolicitud("Sp_GetFlujoAprobadoresSolicitud", tipoSolicitud);
            return model;
        }
    }
}