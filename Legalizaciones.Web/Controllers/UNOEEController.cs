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
        private readonly IEmpleadoPermisoRepository empleadoPermisoRepository;
        private readonly ISolicitudGastosRepository solicitudGastosRepository;
        private Engine.ErpMethod FuncionErp = new Engine.ErpMethod();

        public UNOEEController(IEmpleadoPermisoRepository _empleadoPermisoRepository, ISolicitudGastosRepository solicitudGastosRepository)
        {
            this.empleadoPermisoRepository = _empleadoPermisoRepository;
            this.solicitudGastosRepository = solicitudGastosRepository;
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
            List<Empleado> Empleados = new List<Empleado>();

            Empleados.Add(new Empleado
            {
                Area = "Empleado",
                Nombre = "Eliezer Vargas",
                Cedula = "Nivel3",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100",
                CargoId = 1, // ROL Empleado
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1",
                Correo = "e.vargas@innova4j.com",
                Cargo = "Direccion"
            }) ;

            Empleados.Add(new Empleado
            {
                Area = "Administracion Tesoreria",
                Nombre = "Angelica Betancourt",
                Cedula = "Nivel4",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-2",
                CargoId = 2, // ROL Administracion Tesoreria
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1",
                Correo = "a.betancourt@innova4j.com",
                Cargo = "Gerencia"
            });

            Empleados.Add(new Empleado
            {
                Area = "Administracion Contraloria",
                Nombre = "Daniel Sanchez",
                Cedula = "Nivel5",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3, // ROL Administracion Contraloria
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1",
                Correo = "d.sanchez@innova4j.com",
                Cargo = "Jefatura"
            });

            Empleados.Add(new Empleado
            {
                Area = "Administracion Contabilidad",
                Nombre = "Luz Marina",
                Cedula = "N/A",
                Direccion = "Calle 29 No. 13A - 15. Piso 10",
                Ciudad = "Cucuta",
                Telefono = "(1) 560 00100-3",
                CargoId = 4, // ROL Administracion Contabilidad
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1",
                Correo = "l.marina@innova4j.com",
                Cargo = "Presidencia"
            });

            Empleados.Add(new Empleado
            {
                Area = "Empleado",
                Nombre = "Efrain Mejias",
                Cedula = "N/A",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100",
                CargoId = 1, // ROL Empleado
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1",
                Correo = "e.mejias@innova4j.com",
                Cargo = "VicePresidencia"
            });

            if (filtroCedula)
            {
                var cedula = HttpContext.Session.GetString("Usuario_Cedula");
                var empleadoPermisos = empleadoPermisoRepository.All().Where(m => m.EmpleadoCedula == cedula).ToList();
                var list = Empleados.Where(m => !empleadoPermisos.Any(p => p.EmpleadoPermisoCedula == m.Cedula) && m.Cedula != cedula);
                return Json(list);
            }

            return Json(Empleados);
        }

        public JsonResult CargosMaeEdit(int Id)
        {

            Cargo[] Cargos = new Cargo[3];

            Cargos[0] = new Cargo
            {
                Id = 1,
                Nombre = "Empleado",
                Descripcion = "Departamento de Ventas",
                Estatus = 1
            };

            Cargos[1] = new Cargo
            {
                Id = 2,
                Nombre = "Administracion Tesoreria",
                Descripcion = "Departamento de Compras",
                Estatus = 1
            };

            Cargos[2] = new Cargo
            {
                Id = 3,
                Nombre = "Administracion Contraloria",
                Descripcion = "Gerente de Ventas",
                Estatus = 1
            };



            foreach (var item in Cargos)
            {
                if (item.Id == Id)
                    item.Nombre = item.Nombre + "XX";

            }
            return Json(Cargos);
        }

        public JsonResult Cargos()
        {
            Cargo[] Cargos = new Cargo[4];

            Cargos[0] = new Cargo
            {
                Id = 1,
                Nombre = "Empleado",
                Descripcion = "Departamento de Ventas",
                Estatus = 1
            };

            Cargos[1] = new Cargo
            {
                Id = 2,
                Nombre = "Administracion Tesoreria",
                Descripcion = "Departamento de Compras",
                Estatus = 1
            };

            Cargos[2] = new Cargo
            {
                Id = 3,
                Nombre = "Administracion Contraloria",
                Descripcion = "Gerente de Ventas",
                Estatus = 1
            };

            Cargos[3] = new Cargo
            {
                Id = 3,
                Nombre = "Administracion Contabilidad",
                Descripcion = "Gerente Contable",
                Estatus = 1
            };

            return Json(Cargos);
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
            var OEmpleado = new Empleado();

            switch (wCedula)
            {
                case "6.845.256.665":
                    OEmpleado.Area = "Empleado";
                    OEmpleado.Nombre = "Eliezer Vargas";
                    OEmpleado.Cedula = "6.845.256.665";
                    OEmpleado.Direccion = "Calle 28 No. 13A - 15. Piso 10";
                    OEmpleado.Ciudad = "Bogota";
                    OEmpleado.Telefono = "(1) 560 00100";
                    OEmpleado.CargoId = 1;
                    OEmpleado.CentroOperaciones = "1";
                    OEmpleado.CentroCostos = "1";
                    OEmpleado.UnidadNegocio = "1";
                    OEmpleado.FechaCreacion = DateTime.Now;
                    break;

                case "7.845.256.666":
                    OEmpleado.Area = "Administracion Tesoreria";
                    OEmpleado.Nombre = "Angelica Betancourt";
                    OEmpleado.Cedula = "7.845.256.666";
                    OEmpleado.Direccion = "Calle 28 No. 13A - 15. Piso 10";
                    OEmpleado.Ciudad = "Bogota";
                    OEmpleado.Telefono = "(1) 560 00100-2";
                    OEmpleado.CargoId = 2;
                    OEmpleado.CentroOperaciones = "1";
                    OEmpleado.CentroCostos = "1";
                    OEmpleado.UnidadNegocio = "1";
                    OEmpleado.FechaCreacion = DateTime.Now;
                    break;

                case "8.845.256.667":
                    OEmpleado.Area = "Administracion Contraloria";
                    OEmpleado.Nombre = "Daniel Sanchez";
                    OEmpleado.Cedula = "8.845.256.667";
                    OEmpleado.Direccion = "Calle 28 No. 13A - 15. Piso 10";
                    OEmpleado.Ciudad = "Bogota";
                    OEmpleado.Telefono = "(1) 560 00100-3";
                    OEmpleado.CargoId = 3;
                    OEmpleado.CentroOperaciones = "1";
                    OEmpleado.CentroCostos = "1";
                    OEmpleado.UnidadNegocio = "1";
                    OEmpleado.FechaCreacion = DateTime.Now;
                    break;


                case "9.845.256.668":
                    OEmpleado.Area = "Administracion Contabilidad";
                    OEmpleado.Nombre = "Luz Marina";
                    OEmpleado.Cedula = "9.845.256.668";
                    OEmpleado.Direccion = "Calle 29 No. 13A - 15. Piso 10";
                    OEmpleado.Ciudad = "Cucuta";
                    OEmpleado.Telefono = "(1) 560 00100-3";
                    OEmpleado.CargoId = 4;
                    OEmpleado.CentroOperaciones = "1";
                    OEmpleado.CentroCostos = "1";
                    OEmpleado.UnidadNegocio = "1";
                    OEmpleado.FechaCreacion = DateTime.Now;
                    break;


                case "10.845.256.665":
                    OEmpleado.Area = "Empleado";
                    OEmpleado.Nombre = "Efrain Mejias";
                    OEmpleado.Cedula = "6.845.256.665";
                    OEmpleado.Direccion = "Calle 28 No. 13A - 15. Piso 10";
                    OEmpleado.Ciudad = "Bogota";
                    OEmpleado.Telefono = "(1) 560 00100";
                    OEmpleado.CargoId = 1;
                    OEmpleado.CentroOperaciones = "1";
                    OEmpleado.CentroCostos = "1";
                    OEmpleado.UnidadNegocio = "1";
                    OEmpleado.FechaCreacion = DateTime.Now;
                    break;

                case "11.845.256.665":
                    OEmpleado.Area = "Empleado";
                    OEmpleado.Nombre = "Javier Rodriguez";
                    OEmpleado.Cedula = "6.845.256.665";
                    OEmpleado.Direccion = "Calle 28 No. 13A - 15. Piso 10";
                    OEmpleado.Ciudad = "Bogota";
                    OEmpleado.Telefono = "(1) 560 00100";
                    OEmpleado.CargoId = 1;
                    OEmpleado.CentroOperaciones = "1";
                    OEmpleado.CentroCostos = "1";
                    OEmpleado.UnidadNegocio = "1";
                    OEmpleado.FechaCreacion = DateTime.Now;
                    break;
            }

            var wresul = Json(OEmpleado);
            return Json(wresul);
        }


    }
}