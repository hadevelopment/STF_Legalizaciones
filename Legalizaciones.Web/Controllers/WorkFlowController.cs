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
        public JsonResult UpdatePasoFlujo(string descripcion, string cedula, string nombre , string email, int idPasoFlujoSolicitud, int idDocumento, int idFlujo, string rango, int orden = 0, 
            string aprobadorSuplente1 = "", string cedulaSuplente1 = "",string emailSuplente1 = "", string aprobadorSuplente2 = "", string cedulaSuplente2 = "", string emailSuplente2 = "")
        {
            EngineStf Funcion = new EngineStf();
            int destinoId = Funcion.DestinoId(rango);
            EngineDb Metodo = new EngineDb();
            Metodo.UpdatePasoFlujoAprobacion("Sp_UpdatePasoAprobacion", idPasoFlujoSolicitud, descripcion, cedula, nombre, email,orden, aprobadorSuplente1, cedulaSuplente1,
                                               emailSuplente1,aprobadorSuplente2,cedulaSuplente2,emailSuplente2);
            Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            model = Metodo.AprobadoresFlujoSolicitud("Sp_GetFlujoAprobadoresDocumentos", idDocumento, idFlujo, destinoId);
            return Json(model);
        }

        [HttpPost]
        public JsonResult EliminarPasoFlujo(int idPasoFlujoSolicitud, string tipoDocumento , int idDocumento, int idFlujo , string rango)
        {
            EngineStf Funcion = new EngineStf();
            int destinoId = Funcion.DestinoId(rango);
            EngineDb Metodo = new EngineDb();
            List<DataAprobacion> model = new List<DataAprobacion>();
            Metodo.DeletePasoFlujoAprobacion("Sp_DeletePasoAprobacion", idPasoFlujoSolicitud);
            Funcion.ReordenarFlujoAprobacion(idFlujo);
            model = Metodo.AprobadoresFlujoSolicitud("Sp_GetFlujoAprobadoresDocumentos", idDocumento, idFlujo, destinoId);
            return Json(model);
        }

        [HttpGet]
        public JsonResult ExisteDocumentosAsociados(int idPasoFlujoSolicitud, string tipoDocumento)
        {
            DataAprobacion model = new DataAprobacion();
            EngineDb Metodo = new EngineDb();
            model.Id = Metodo.CountDocAsociado("Sp_GetCountDocAsociadoPaso", idPasoFlujoSolicitud, tipoDocumento);
            return Json(model);
        }

        [HttpGet]
        public JsonResult ExisteRangoAprobacion(int destinoId, float montoMaximo, float montoMinimo, int idDocumento)
        {
            DataAprobacion model = new DataAprobacion();
            EngineDb Metodo = new EngineDb();
            model.Id = Metodo.ExisteRangoAprobacion("Sp_GetExisteRangoAprobacion", destinoId ,montoMinimo, montoMaximo, idDocumento);
            return Json(model);
        }

        [HttpPost]
        public JsonResult CreateFlujoDocumento(int paso=0, string tipoDocumento="",int idDocumento=0,string aprobador="",string empleado="",string descripcion="",string mail="",
                                               string destino="",int destinoId=0,float montoMaximo=0,float montoMinimo=0, string aprobadorSuplente1 = "", string cedulaSuplente1= "",
                                               string emailSuplente1 = "" , string aprobadorSuplente2 = "", string cedulaSuplente2 = "",string  emailSuplente2 = "")
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
                model = Funcion.SetCreateAprobadorFlujo(model, tipoDocumento,idDocumento, JefeArea[0], JefeArea[1], JefeArea[2], JefeArea[3], update , estatus, paso, destinoId, montoMaximo, montoMinimo,
                     aprobadorSuplente1, cedulaSuplente1, emailSuplente1, aprobadorSuplente2, cedulaSuplente2 , emailSuplente2);
            }
            else
            {
                model = Funcion.SetCreateAprobadorFlujo(model, tipoDocumento,idDocumento, aprobador, empleado,descripcion, mail, update, estatus, paso, destinoId, montoMaximo, montoMinimo ,
                    aprobadorSuplente1, cedulaSuplente1, emailSuplente1, aprobadorSuplente2, cedulaSuplente2, emailSuplente2);
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