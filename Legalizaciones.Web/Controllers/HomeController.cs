using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Models;
using Legalizaciones.Interface;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Web.Helpers;
using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Legalizaciones.Web.Controllers
{
    public class HomeController : Controller
    {
        //IJerarquia
        private readonly IPaisRepository paisRepository;
        //ISolicitud
        private readonly IConceptoRepository conceptoRepository;
        private readonly IDestinoRepository destinoRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly IZonaRepository zonaRepository;
        
        private readonly ISolicitudRepository solicitudRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private IConfiguration _config;

        public HomeController(ISolicitudRepository solicitudRepository, IPaisRepository paisRepository, IConceptoRepository conceptoRepository, IDestinoRepository destinoRepository,
                              IMonedaRepository monedaRepository, IZonaRepository zonaRepository, ISolicitudGastosRepository solicitudGastosRepository, IConfiguration Configuration)
        {
            this.solicitudRepository = solicitudRepository;
            this.paisRepository = paisRepository;
            this.conceptoRepository = conceptoRepository;
            this.destinoRepository = destinoRepository;
            this.monedaRepository = monedaRepository;
            this.zonaRepository = zonaRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            _config = Configuration;
        }
        public IActionResult Index()
        {
            Empleado emp = new Empleado();
            UNOEE erp = new UNOEE();

            //Busco el empleado logeado del AppSettings
            int wEmpleadoLogeado = Convert.ToInt32(_config["AppSettings:EmpLog"]);

            //emp = erp.getEmpleadoID(erp.Empleado_logeado());
            emp = erp.getEmpleadoID(wEmpleadoLogeado);
            HttpContext.Session.SetString("Usuario_Nombre", emp.Nombre);
            HttpContext.Session.SetString("Usuario_Area", emp.Area);
            HttpContext.Session.SetString("Usuario_Cedula", emp.Cedula);
            HttpContext.Session.SetString("Usuario_Cargo", emp.CargoId.ToString());
            HttpContext.Session.SetString("Usuario_Telefono", emp.Telefono);
            HttpContext.Session.SetString("Usuario_Ciudad", emp.Ciudad);
            HttpContext.Session.SetString("Usuario_Direccion", emp.Direccion);

            return View();
        }

       
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        /*Json que retorna los coneptos*/
        public JsonResult Conceptos()
        {
            return Json(conceptoRepository.All());
        }
        /*Json que retorna los destinos*/
        public JsonResult Destinos()
        {
            return Json(destinoRepository.All());
        }
        /*Json que retorna los zonas*/
        public JsonResult Zonas()
        {
            return Json(zonaRepository.All());
        }
        /*Json que retorna las monedas*/
        public JsonResult Monedas()
        {
            return Json(monedaRepository.All());
        }
        /*Json que retorna las origen*/
        public JsonResult Origen()
        {
            return Json(destinoRepository.All());
        }
        /*Json que retorna las Destino*/
        public JsonResult Destino()
        {
            return Json(destinoRepository.All());
        }
    }
}
