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
            EngineStf Funcion = new EngineStf();
            //var resultado = Funcion.UseKactusAsync();
            var UnidadNegocio = Servicio.UnidadNegocioSingleAsync("01");
            // var UnidadesNegocios = Servicio.UnidadNegocioCollectionAsync();
            // var centroOperacion = Servicio.CentroOperacionesSingleAsync("27");
            // var centroOperacion = Servicio.CentroOperacionesCollectionAsync();
            //var tipoImpuesto = Servicio.TipoImpuestosSingleAsync("01");
            //var tipoImpuesto = Servicio.TipoImpuestosSingleAsync("CS098");
            //var proveedor = Servicio.ProveedoresSingleAsync("1130609205");
            //var proveedor = Servicio.ProveedoresCollectionAsync();
            //var tipoServicio = Servicio.TipoServiciosSingleAsync("AF001");
            //var tipoServicio = Servicio.TiposServiciosCollectionAsync();
           // var centroCosto = Servicio.CentroCostosCollectionAsync();
           // var centroCosto = Servicio.CentroCostosSingleAsync("020111");
            return View();
        }
    }
}