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
        public IActionResult Index(AprobacionDocumento model)
        {
            if (HttpContext.Session.GetString("Usuario_Cedula") == null)
                return RedirectToAction("Index", "Home");

      
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(AprobacionDocumento model , string z = "")
        {

            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateFlujo(int id = 0, int orden = 0, string descripcionT = "", string nombre = "", string email = "", string cedula = "", string type = "",
                                         int flujoSolicituIde = 0, int destinoIde = 0, float maximo = 0 ,float  minimo = 0)
        {
            EngineDb Metodo = new EngineDb();
            Metodo.UpdatePasoFlujoAprobacion("Sp_UpdatePasoAprobacion", id, descripcionT, cedula, nombre, email);
            AprobacionDocumento model = new AprobacionDocumento();
            model.TipoSeleccionado = type;
            return RedirectToAction("Index", "WorkFlow", model);
        }

        [HttpPost]
        public IActionResult EliminarFlujo(int ide = 0, string typee = "", int flujoSolicituIdee = 0, int destinoIdee = 0, float maximoe = 0, float minimoe = 0)
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

        private int ExisteRegistroEnPaso(int paso , string tipoDocumento, float montoMinimo, float montoMaximo)
        {
            int resultado = -1;
            EngineDb Metodo = new EngineDb();
            if (paso == 0)
            {
                if (montoMinimo <= 0 || montoMaximo <= 0 || montoMinimo < montoMaximo) return resultado;
            }
            resultado = Metodo.ExistePasoFlujoAprobacion("Sp_GetExistePasoFlujo", paso, tipoDocumento, montoMinimo, montoMaximo);
            return resultado;
        }

        /* private AprobacionDocumento GetTipoSolicitudes(AprobacionDocumento model)
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
         }*/

        [HttpGet]
        public JsonResult ExisteRangoAprobacion(int destinoId, float montoMaximo, float montoMinimo, int idDocumento)
        {
            DataAprobacion model = new DataAprobacion();
            EngineDb Metodo = new EngineDb();
            model.Id = Metodo.ExisteRangoAprobacion("Sp_GetExisteRangoAprobacion", destinoId ,montoMinimo, montoMaximo, idDocumento);
            return Json(model);
        }

        [HttpPost]
        public JsonResult CreateFlujoDocumento(int paso=0, string tipoDocumento="",int idDocumento=0,string aprobador="",string empleado="",string descripcion="",string mail="",string destino="",int destinoId=0,float montoMaximo=0,float montoMinimo=0)
        {
            List<Legalizaciones.Web.Models.DataAprobacion> model = new List<Legalizaciones.Web.Models.DataAprobacion>();
            EngineStf Funcion = new EngineStf();
            EngineDb Metodo = new EngineDb();
            int update=0;
            int estatus=1;
            if (paso == 1)
            {
                update++;
                string[] JefeArea = Funcion.SimuladorKactusJefeInmediato();
                model = Funcion.SetCreateAprobadorFlujo(model, tipoDocumento,idDocumento, JefeArea[0], JefeArea[1], JefeArea[2], JefeArea[3], update , estatus, paso, destinoId, montoMaximo, montoMinimo);
            }
            else
            {
                model = Funcion.SetCreateAprobadorFlujo(model, tipoDocumento,idDocumento, aprobador, empleado,descripcion, mail, update, estatus, paso, destinoId, montoMaximo, montoMinimo);
            }
            return Json(model);
        }

        [HttpGet]
        public JsonResult GetAprobadores(int idDocumento , int idFlujo , string rango)
        {
            EngineStf Funcion = new EngineStf();
            int destinoId = Funcion.DestinoId(rango);
            Engine.EngineDb Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            model = Metodo.AprobadoresFlujoSolicitud("Sp_GetFlujoAprobadoresDocumentos", idDocumento , idFlujo, destinoId);
            return Json(model);
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

        [HttpGet]
        public JsonResult GetDocumentType()
        {
            EngineDb Metodo = new EngineDb();
            List<DocumentoTipo> List = Metodo.DocumentType("Sp_GetDocumentType");
            return Json(List);
        }

        [HttpGet]
        public JsonResult GetFlujos(int id)
        {
            EngineDb Metodo = new EngineDb();
            List<FlujoDescripcion> List = Metodo.DescripcionFlujo("Sp_GetDescripcionFlujo",id);
            return Json(List);
        }



    }
}