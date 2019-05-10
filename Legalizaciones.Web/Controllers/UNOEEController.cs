using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Mvc;

namespace Legalizaciones.Web.Controllers
{
    public class UNOEEController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

        public JsonResult Servicios(int ? proveedorId)
        {

            Servicio[] lstServicios = new Servicio[3];

            lstServicios[0] = new Servicio
            {
                Id = 1,
                Nombre = "Comida"
            };
            lstServicios[1] = new Servicio
            {
                Id = 2,
                Nombre = "Transporte"
            };
            lstServicios[2] = new Servicio
            {
                Id = 3,
                Nombre = "Hospedaje"
            };


            //List<Servicio> lstServicios = new List<Servicio>();
            //Servicio servicio = new Servicio();
            //servicio.Id = 1;
            //servicio.Nombre = "Comida";

            //Servicio servicio2 = new Servicio();
            //servicio2.Id = 2;
            //servicio2.Nombre = "Transporte";

            //lstServicios.Add(servicio);
            //lstServicios.Add(servicio2);

            return Json(lstServicios);
        }

        public JsonResult MontoServicio(int servicioId)
        {
            ServicioDetalle servicioDetalle = new ServicioDetalle();
            servicioDetalle.Id = 1;
            servicioDetalle.Monto = 30000;

            return Json(servicioDetalle);
        }

        public JsonResult CentroOperaciones()
        {
            List<CentroOperacion> lstCentroOperaciones = new List<CentroOperacion>();
            CentroOperacion centroOperacion = new CentroOperacion();
            centroOperacion.Id = 1;
            centroOperacion.Nombre = "Centro Operaciones 01";

            lstCentroOperaciones.Add(centroOperacion);

            return Json(lstCentroOperaciones);
        }

        public JsonResult UnidadNegocios()
        {
            List<UnidadNegocio> lstUnidadNegocio = new List<UnidadNegocio>();
            UnidadNegocio unidadNegocio = new UnidadNegocio();
            unidadNegocio.Id = 3;
            unidadNegocio.Nombre = "Unidad Negocio 01";

            lstUnidadNegocio.Add(unidadNegocio);

            return Json(lstUnidadNegocio);
        }

        public JsonResult CentroCostos()
        {
            List<CentroCosto> lstCentroCosto = new List<CentroCosto>();
            CentroCosto centroCosto = new CentroCosto();
            centroCosto.Id = 3;
            centroCosto.Nombre = "Centro Costo. 01";

            lstCentroCosto.Add(centroCosto);

            return Json(lstCentroCosto);
        }

        public JsonResult Empleados()
        {
            Empleado[] Empleados = new Empleado[3];

            Empleados[0] = new Empleado
            {
                Area = "Empleado",
                Nombre = "Juan Perez",
                Cedula = "6.845.256.665",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100",
                CargoId = 1 // ROL Empleado
            };

            Empleados[1] = new Empleado
            {
                Area = "Administracion Tesoreria",
                Nombre = "Angelica Betancourt",
                Cedula = "7.845.256.666",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-2",
                CargoId = 2 // ROL Administracion Tesoreria
            };

            Empleados[2] = new Empleado
            {
                Area = "Administracion Contraloria",
                Nombre = "Daniel Sanchez",
                Cedula = "7.845.256.667",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3 // ROL Administracion Contraloria
            };

            return Json(Empleados);
        }

        public JsonResult Cargos()
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
    }
}