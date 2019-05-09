using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Models;
using Legalizaciones.Interface;
using Legalizaciones.Interface.IEmpresa;
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
        //IEmpresa
        private readonly ICompaniaRepository companiaRepository;
        private readonly IGerenciaRepository gerenciaRepository;
        private readonly ISupervicionRepository supervicionRepository;
        //IJerarquia
        private readonly IPaisRepository paisRepository;
        //ISolicitud
        private readonly IConceptoRepository conceptoRepository;
        private readonly IDestinoRepository destinoRepository;
        private readonly IMonedaRepository monedaRepository;
        private readonly IZonaRepository zonaRepository;
        //Interface
        private readonly IProductoRepository productoRepository;
        private readonly ICategoriaRepository categoryRepository;
        private readonly IClienteRepository clienteRepository;
        private readonly IInventarioRepository inventoryRepository;
        private readonly IServicioRepository servicioRepository;
        
        private readonly IProveedorRepository proveedorRepository;
        private readonly ISolicitudRepository solicitudRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private IConfiguration _config;

        public HomeController(IProductoRepository productoRepository, ICategoriaRepository categoryRepository,
                              IClienteRepository clienteRepository, IInventarioRepository inventoryRepository, IServicioRepository servicioRepository, IProveedorRepository proveedorRepository, ISolicitudRepository solicitudRepository, 
                              ICompaniaRepository companiaRepository, IGerenciaRepository gerenciaRepository, ISupervicionRepository supervicionRepository, IPaisRepository paisRepository, IConceptoRepository conceptoRepository, IDestinoRepository destinoRepository,
                              IMonedaRepository monedaRepository, IZonaRepository zonaRepository, ISolicitudGastosRepository solicitudGastosRepository, IConfiguration Configuration)
        {
            this.productoRepository = productoRepository;
            this.categoryRepository = categoryRepository;
            this.clienteRepository = clienteRepository;
            this.inventoryRepository = inventoryRepository;
            this.servicioRepository = servicioRepository;
            this.proveedorRepository = proveedorRepository;
            this.solicitudRepository = solicitudRepository;
            this.companiaRepository = companiaRepository;
            this.gerenciaRepository = gerenciaRepository;
            this.supervicionRepository = supervicionRepository;
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

        [HttpGet]
        public ActionResult Order()
        {
            return View(new Inventario());
        }

        [HttpPost]
        public ActionResult Order(Inventario data)
        {
            if (!ModelState.IsValid && data.ClienteID == 0)
                return View(data);
            
            inventoryRepository.Insert(data);
            return RedirectToAction("Index", "Home");
        } 

        public IActionResult CustomerList()
        {
            return View(clienteRepository.All()); 
        }
        public IActionResult CategoryList()
        {
            return View(categoryRepository.All()); 
        }
        public IActionResult ProveedorList()
        {
            return View(proveedorRepository.All());
        }
        public IActionResult ProductList()
        {
            return View(productoRepository.All());
        }
        
        public IActionResult ServicioList()
        {
            return View(servicioRepository.All());
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public JsonResult Clientes()
        {
            return Json(clienteRepository.All());
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
        public JsonResult GetCategories()
        {
            return Json(categoryRepository.All());
        }
        public JsonResult GetProveedores()
        {
            return Json(proveedorRepository.All());
        }
        public JsonResult GetProducts(int categoryId = 0)
        {
            if (categoryId == 0)
                return Json(productoRepository.All());
            return Json(productoRepository.All().Where(x => x.CategoriaID == categoryId));
        }
        public JsonResult GetProductById(int productId)
        {
            return Json(productoRepository.All().Where(x => x.Id == productId));
        }
        //Por mi
        public JsonResult GetServicios(int servicioId = 0)
        {
            if (servicioId == 0)
                return Json(servicioRepository.All());
            return Json(servicioRepository.All().Where(x => x.ProveedorID == servicioId));
        }
        public JsonResult GetServicioById(int servicioId)
        {
            return Json(servicioRepository.All().Where(x => x.Id == servicioId));
        }
    }
}
