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
using Legalizaciones.Web.Models.ViewModel;

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

        private readonly IKactusEmpleadoRepository kactusEmpleadoRepository;

        public UNOEE erp = new UNOEE();

        public HomeController(ISolicitudRepository solicitudRepository, IPaisRepository paisRepository, IConceptoRepository conceptoRepository, IDestinoRepository destinoRepository,
                              IMonedaRepository monedaRepository, IZonaRepository zonaRepository, ISolicitudGastosRepository solicitudGastosRepository, IConfiguration Configuration,
                              IKactusEmpleadoRepository kactusEmpleadoRepository)
        {
            this.solicitudRepository = solicitudRepository;
            this.paisRepository = paisRepository;
            this.conceptoRepository = conceptoRepository;
            this.destinoRepository = destinoRepository;
            this.monedaRepository = monedaRepository;
            this.zonaRepository = zonaRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.kactusEmpleadoRepository = kactusEmpleadoRepository;
            _config = Configuration;
        }
        public IActionResult Index()
        {
            //si no existe una variable de sesion creada del empleado cargo la que viene x defecto del archivo appsettings.json
            if (HttpContext.Session.GetString("Usuario_Cargo") == null)
            {
                KactusEmpleado emp = new KactusEmpleado();
                

                //Busco el empleado logeado del AppSettings
                string wEmpleadoLogeado = Convert.ToString(_config["AppSettings:EmpLog"]);
                string wEmpleadoRol = Convert.ToString(_config["AppSettings:EmpRol"]);

                //emp = erp.getEmpleadoID(erp.Empleado_logeado());
                emp = kactusEmpleadoRepository.getEmpleadoCedula(wEmpleadoLogeado);
                HttpContext.Session.SetString("Usuario_Nombre", emp.PrimerNombre + " " + emp.PrimerApellido);
                HttpContext.Session.SetString("Usuario_Area", emp.CodigoArea);
                HttpContext.Session.SetString("Usuario_Cedula", emp.NumeroDeIdentificacion);
                HttpContext.Session.SetString("Usuario_Cargo", emp.CargoEmpleado);
                HttpContext.Session.SetString("Usuario_Telefono", emp.Celular);
                HttpContext.Session.SetString("Usuario_Direccion", emp.Direccion);
                HttpContext.Session.SetString("Usuario_Rol", wEmpleadoRol);
            }

            var ListaEmpleado = kactusEmpleadoRepository.All();

            var wHomevista = new HomeViewModel
            {
                ListaEmpleados = new SelectList(ListaEmpleado, "Cedula", "Nombre"),
            };

            return View(wHomevista);
        }


        [HttpGet]
        public IActionResult CambiarUsuario(string Cedula)
        {

            if (ModelState.IsValid){
                var Oempleado = kactusEmpleadoRepository.getEmpleadoCedula(Cedula);
                HttpContext.Session.SetString("Usuario_Nombre", Oempleado.PrimerNombre + " " + Oempleado.PrimerApellido);
                HttpContext.Session.SetString("Usuario_Area", Oempleado.CodigoArea);
                HttpContext.Session.SetString("Usuario_Cedula", Oempleado.NumeroDeIdentificacion);
                HttpContext.Session.SetString("Usuario_Cargo", Oempleado.CargoEmpleado);
                HttpContext.Session.SetString("Usuario_Telefono", Oempleado.Celular);
                HttpContext.Session.SetString("Usuario_Direccion", Oempleado.Direccion);
            }

            return RedirectToAction("Index");
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
