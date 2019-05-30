using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Models;
using Legalizaciones.Web.Engine;
using Microsoft.AspNetCore.Http;

namespace Legalizaciones.Web.Controllers
{
    public class WorkFlowController : Controller
    {
        [HttpGet]
        public IActionResult Index(AprobacionDocumento model, string tipoDocumento = "", string addAprobador = "", string addMail = "",
                                    string empleado = "", string descripcion = "", int paso = 0, string clear = "", string addPaso = "")
        {
            if (HttpContext.Session.GetString("Usuario_Cedula") == null)
                return RedirectToAction("Index", "Home");

            model = GetTipoSolicitudes(model);
            if (HttpContext.Session.GetString("IndiceSolicitud") != string.Empty && HttpContext.Session.GetString("IndiceSolicitud") != null)
                ViewBag.IndexSollicitud = HttpContext.Session.GetString("IndiceSolicitud");
            if ((tipoDocumento == null || tipoDocumento == string.Empty) && (HttpContext.Session.GetString("TipoDocumento") != null && HttpContext.Session.GetString("TipoDocumento") != string.Empty))
            {
                tipoDocumento = HttpContext.Session.GetString("TipoDocumento");
                model.TipoSeleccionado = tipoDocumento;
            }
            else
            {
                model.TipoSeleccionado = tipoDocumento;
            }
            //CREAR INSERTAR NUEVO PASO
            ViewBag.Paso = 0;
            if (tipoDocumento != string.Empty && tipoDocumento != null && addAprobador != string.Empty && addAprobador != null && addMail != string.Empty && addMail != null
                                                                      && empleado != string.Empty && empleado != null && descripcion != string.Empty && descripcion != null)
            {
                int estatus = 1;
                int update = 0;
                EngineDb Metodo = new EngineDb();
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

                int existePaso = Metodo.ExistePasoFlujoAprobacion("Sp_GetExistePasoFlujo", paso, tipoDocumento);
                if (existePaso == 0)
                {
                    model = Funcion.SetCreateAprobador(model, tipoDocumento, addAprobador, empleado, descripcion, addMail, update, estatus, paso);
                }
                else if (existePaso > 0)
                {
                    model.FlujoAprobacion = GetAprobadores(tipoDocumento);
                }

                if (model.FlujoAprobacion != null)
                    ViewBag.Paso = model.FlujoAprobacion.Count + 1;
            }
            //AGREGAR PASO AL FLUJO DE APROBACION
            if (addPaso != string.Empty)
            {
                model.FlujoAprobacion = GetAprobadores(tipoDocumento);
                if (model.FlujoAprobacion != null)
                    ViewBag.Paso = model.FlujoAprobacion.Count + 1;
            }
            //LISTA DE PASOS DE APROBACION
            if (model.TipoSeleccionado != string.Empty && model.TipoSeleccionado != null)
            {
                model.Aprobadores = GetAprobadores(model.TipoSeleccionado);
                ViewBag.AprobadoresCount = model.Aprobadores.Count;
                if (model.Aprobadores.Count == 0)
                    model.Aprobadores = null;
            }
            ViewBag.TipoDocumento = tipoDocumento;

            //FINALIZAR CREACION DE FLUJO DE APROBACION
            if (clear != string.Empty)
            {
                ViewBag.Paso = 0;
                ViewBag.TipoDocumento = null;
                model.FlujoAprobacion = null;
            }
            //ELIMINACION DE PASO EN EL FLUJO DE APROBACION
            if (model.CountDocAsociado > 0)
            {
                ViewBag.CountDocAsociado = model.CountDocAsociado;
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string tipo = "", int n = 0)
        {
            if (HttpContext.Session.GetString("Usuario_Cedula") == null)
                return RedirectToAction("Index", "Home");

            AprobacionDocumento model = new AprobacionDocumento();
            if (tipo == string.Empty)
                model.TipoSeleccionado = Request.Form["tipoSolicitud"];
            else
                model.TipoSeleccionado = tipo;

            ViewBag.IndexSollicitud = Request.Form["indiceSolicitud"].ToString();
            HttpContext.Session.SetString("IndiceSolicitud", Request.Form["indiceSolicitud"].ToString());
            model = GetTipoSolicitudes(model);
            if (model.TipoSeleccionado != "Seleccione...")
            {
                HttpContext.Session.SetString("TipoDocumento", model.TipoSeleccionado);
                model.Aprobadores = GetAprobadores(model.TipoSeleccionado);
                ViewBag.AprobadoresCount = model.Aprobadores.Count;
                if (model.Aprobadores.Count == 0)
                    model.Aprobadores = null;
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateFlujo(int id = 0, int orden = 0, string descripcionT = "", string nombre = "", string email = "", string cedula = "", string type = "")
        {
            EngineDb Metodo = new EngineDb();
            Metodo.UpdatePasoFlujoAprobacion("Sp_UpdatePasoAprobacion", id, descripcionT, cedula, nombre, email);
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = type;
            return RedirectToAction("Index", "WorkFlow", model);
        }

        [HttpPost]
        public IActionResult EliminarFlujo(int ide = 0, string typee = "")
        {
            EngineDb Metodo = new EngineDb();
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = typee;
            model.CountDocAsociado = Metodo.CountDocAsociado("Sp_GetCountDocAsociadoPaso", ide, typee);
            if (model.CountDocAsociado > 0)
            {
                return RedirectToAction("Index", "WorkFlow", model);
            }
            Metodo.DeletePasoFlujoAprobacion("Sp_DeletePasoAprobacion", ide);
            EngineStf Funcion = new EngineStf();
            Funcion.ReordenarFlujoAprobacion(typee);
            return RedirectToAction("Index", "WorkFlow", model);
        }

        private AprobacionDocumento GetTipoSolicitudes(AprobacionDocumento model)
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

        [HttpGet]
        public JsonResult GetDestino()
        {
            EngineDb Metodo = new EngineDb();
            List<Destinos> List = Metodo.Destino("Sp_GetDestino");
            return Json(List);
        }

        [HttpGet]
        public JsonResult GetMoneda(int id)
        {
            EngineDb Metodo = new EngineDb();
            Monedas[] List = Metodo.Moneda("Sp_GetMoneda",id);
            return Json(List);
        }


    }
}