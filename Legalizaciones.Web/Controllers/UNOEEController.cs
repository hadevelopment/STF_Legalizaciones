using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Data.Repository;
using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Erp.Models;


namespace Legalizaciones.Web.Controllers
{
    public class UNOEEController : Controller
    {
        private readonly IKactusEmpleadoRepository kactusEmpleadoRepository;
        private readonly IEmpleadoPermisoRepository empleadoPermisoRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private Engine.ErpMethod FuncionErp = new Engine.ErpMethod();

        public UNOEEController(IEmpleadoPermisoRepository _empleadoPermisoRepository, ISolicitudGastosRepository solicitudGastosRepository, IKactusEmpleadoRepository kactusEmpleadoRepository)
        {
            this.empleadoPermisoRepository = _empleadoPermisoRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
            this.kactusEmpleadoRepository = kactusEmpleadoRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<JsonResult> GetProveedores()
        {
            ListSuppliers Proveedores = new ListSuppliers();
            Proveedores = await FuncionErp.ProveedoresCollectionAsync();
            return Json(Proveedores);
        }


        [HttpGet]
        public async Task<JsonResult> GetProveedor(string id)
        {
            Suppliers Proveedores = new Suppliers();
            Proveedores = await FuncionErp.ProveedoresSingleAsync(id);
            return Json(Proveedores);
        }

        [HttpGet]
        public async Task<JsonResult> GetServicios()
        {
            ListServiceTypes Servicio = new ListServiceTypes();
            Servicio = await FuncionErp.TiposServiciosCollectionAsync();
            return Json(Servicio);
        }

        [HttpGet]
        public async Task<JsonResult> GetServicio(string id)
        {
            ServiceTypes Servicio = new ServiceTypes();
            Servicio = await FuncionErp.TipoServiciosSingleAsync(id);
            return Json(Servicio);
        }

        [HttpGet]
        public async Task<JsonResult> GetCentroOperaciones()
        {
            ListOperationCenter CentroOperaciones = new ListOperationCenter();
            CentroOperaciones = await FuncionErp.CentroOperacionesCollectionAsync();
            return Json(CentroOperaciones);
        }

        [HttpGet]
        public async Task<JsonResult> GetCentroOperacion(string id)
        {
            OperationCenter CentroOperacion = new OperationCenter();
            CentroOperacion = await FuncionErp.CentroOperacionesSingleAsync(id);
            return Json(CentroOperacion);
        }

        [HttpGet]
        public async Task<JsonResult> GetUnidadNegocios()
        {
            ListBussinesUnit UnidadNegocio = new ListBussinesUnit();
            UnidadNegocio = await FuncionErp.UnidadNegocioCollectionAsync();
            return Json(UnidadNegocio);
        }

        [HttpGet]
        public async Task<JsonResult> GetUnidadNegocio(string id)
        {
            BusinessUnit UnidadNegocio = new BusinessUnit();
            UnidadNegocio = await FuncionErp.UnidadNegocioSingleAsync(id);
            return Json(UnidadNegocio);
        }

        [HttpGet]
        public async Task<JsonResult>  GetCentroCostos()
        {
            ListCostCenters CentroCosto = new ListCostCenters();
            CentroCosto = await FuncionErp.CentroCostosCollectionAsync();
            return Json(CentroCosto);
        }

        [HttpGet]
        public async Task<JsonResult> GetCentroCosto(string id)
        {
            ListCostCenters CentroCosto = new ListCostCenters();
            CentroCosto = await FuncionErp.CentroCostosCollectionAsync();
            return Json(CentroCosto);
        }

        [HttpGet]
        public async Task<JsonResult> GetTipoImpuesto(string id)
        {
            TaxTypes TipoImpuesto = new TaxTypes();
            TipoImpuesto = await FuncionErp.TipoImpuestosSingleAsync(id);
            return Json(TipoImpuesto);
        }

        public JsonResult Empleados(Boolean filtroCedula)
        {
            List<KactusEmpleado> Empleados = new List<KactusEmpleado>();
            Empleados = kactusEmpleadoRepository.All().ToList();

            if (filtroCedula)
            {
                var cedula = HttpContext.Session.GetString("Usuario_Cedula");
                var empleadoPermisos = empleadoPermisoRepository.All().Where(m => m.EmpleadoCedula == cedula).ToList();
                var list = Empleados.Where(m => !empleadoPermisos.Any(p => p.EmpleadoPermisoCedula == m.NumeroDeIdentificacion) && m.NumeroDeIdentificacion != cedula);
                return Json(list);
            }
            else
            {
                return Json(Empleados);
            }
        }

        public JsonResult OrigenDestinosPais(string paisID)
        {
            OrigenDestino[] OrigenDestinos = new OrigenDestino[4];

            OrigenDestinos[0] = new OrigenDestino
            {
                Id = 1,
                Nombre = "Cali",
            };

            OrigenDestinos[1] = new OrigenDestino
            {
                Id = 2,
                Nombre = "Bogota",
            };

            OrigenDestinos[2] = new OrigenDestino
            {
                Id = 3,
                Nombre = "Medellin",
            };

            OrigenDestinos[3] = new OrigenDestino
            {
                Id = 4,
                Nombre = "Cucuta",
            };

            return Json(OrigenDestinos);
        }


        public JsonResult GetEmpleadoByCedula(string wCedula)
        {
            var KactusEmpleado = kactusEmpleadoRepository.All().Where(m => m.NumeroDeIdentificacion == wCedula).Single();
            return Json(KactusEmpleado);
        }

        /*DATOS DEMO*/

        public JsonResult Servicios(int? proveedorId)
        {

            Servicio[] lstServicios = new Servicio[4];

            lstServicios[0] = new Servicio
            {
                Id = 1,
                Nombre = "ALIMENTACION"
            };
            lstServicios[1] = new Servicio
            {
                Id = 2,
                Nombre = "SERVICIO DE TRANSPORTE"
            };
            lstServicios[2] = new Servicio
            {
                Id = 3,
                Nombre = "SERVICIO DE TRASLADO"
            };
            lstServicios[3] = new Servicio
            {
                Id = 4,
                Nombre = "HOSPEDAJE"
            };

            return Json(lstServicios);
        }

        public JsonResult CentroOperaciones()
        {
            List<CentroOperacion> lstCentroOperaciones = new List<CentroOperacion>();
            CentroOperacion centroOperacion = new CentroOperacion();
            centroOperacion.Id = 1;
            centroOperacion.Nombre = "Centro Operaciones 01";

            CentroOperacion centroOperacion2 = new CentroOperacion();
            centroOperacion2.Id = 2;
            centroOperacion2.Nombre = "Centro Operaciones 02";

            lstCentroOperaciones.Add(centroOperacion);
            lstCentroOperaciones.Add(centroOperacion2);

            return Json(lstCentroOperaciones);
        }

        public JsonResult UnidadNegocios()
        {
            List<UnidadNegocio> lstUnidadNegocio = new List<UnidadNegocio>();
            UnidadNegocio unidadNegocio = new UnidadNegocio();
            unidadNegocio.Id = 1;
            unidadNegocio.Nombre = "Unidad Negocio 01";

            UnidadNegocio unidadNegocio2 = new UnidadNegocio();
            unidadNegocio2.Id = 2;
            unidadNegocio2.Nombre = "Unidad Negocio 02";

            lstUnidadNegocio.Add(unidadNegocio);
            lstUnidadNegocio.Add(unidadNegocio2);

            return Json(lstUnidadNegocio);
        }

        public JsonResult CentroCostos()
        {
            List<CentroCosto> lstCentroCosto = new List<CentroCosto>();
            CentroCosto centroCosto = new CentroCosto();
            centroCosto.Id = 1;
            centroCosto.Nombre = "Centro Costo 01";

            CentroCosto centroCosto2 = new CentroCosto();
            centroCosto2.Id = 2;
            centroCosto2.Nombre = "Centro Costo 02";

            lstCentroCosto.Add(centroCosto);
            lstCentroCosto.Add(centroCosto2);

            return Json(lstCentroCosto);
        }

        public JsonResult Proveedores()
        {
            List<Proveedor> lstProveedores = new List<Proveedor>();
            Proveedor proveedor = new Proveedor();
            proveedor.Id = 1;
            proveedor.Nombre = "Proveedor Prueba";
            lstProveedores.Add(proveedor);

            return Json(lstProveedores);
        }

        

    }
}