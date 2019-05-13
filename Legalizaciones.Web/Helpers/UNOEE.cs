using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Helpers
{
    public class UNOEE
    {

        public int Empleado_logeado()
        {
            // 1= ROL Empleado
            // 2= ROL Administracion Tesoreria
            // 3= ROL Administracion Contraloria

            int EL_Empleado = 2;
            return EL_Empleado;
        }
        public CentroCosto getCentroCosto(int IdCentroCosto) {
            CentroCosto CentroCostos = new CentroCosto();
            CentroCostos.Id = 1;
            CentroCostos.Nombre = "Centro de Costo";
            return CentroCostos;
        }

        public CentroOperacion getCentroOperacion(int IdCentroOperacion)
        {
            CentroOperacion centroOperacion = new CentroOperacion();
            centroOperacion.Id = 1;
            centroOperacion.Nombre = "Centro de Operacion";
            return centroOperacion;
        }


        public UnidadNegocio getUnidadNegocio(int IdUnidadNegocio)
        {
            UnidadNegocio unidadNegocio = new UnidadNegocio();
            unidadNegocio.Id = 1;
            unidadNegocio.Nombre = "Unidad de Negocio";
            return unidadNegocio;
        }

        public Empleado getEmpleadoID(int IdEmpleado)
        {
            Empleado[] Empleados = new Empleado[3];


            Empleados[0] = new Empleado
            {
                Area = "Empleado",
                Nombre = "Eliezer Vargas",
                Cedula = "6.845.256.665",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100",
                CargoId = 1, // ROL Empleado
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            Empleados[1] = new Empleado
            {
                Area = "Administracion Tesoreria",
                Nombre = "Angelica Betancourt",
                Cedula = "7.845.256.666",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-2",
                CargoId = 2, // ROL Administracion Tesoreria
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            Empleados[2] = new Empleado
            {
                Area = "Administracion Contraloria",
                Nombre = "Daniel Sanchez",
                Cedula = "8.845.256.667",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3, // ROL Administracion Contabilidad
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };


            var result = Empleados.Where(e => e.CargoId == IdEmpleado).FirstOrDefault();
            return (result);
        }
        public Empleado getEmpleadoCedula(string cedula)
        {
            Empleado[] Empleados = new Empleado[3];

            int ind = 1;
            switch (cedula)
            {
                case "6.845.256.665":
                    ind = 1;
                    break;
                case "7.845.256.666":
                    ind = 2;
                    break;
                case "8.845.256.667":
                    ind = 3;
                    break;
            }

            Empleados[0] = new Empleado
            {
                Area = "Empleado",
                Nombre = "Eliezer Vargas",
                Cedula = "6.845.256.665",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100",
                CargoId = 1, // ROL Empleado
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            Empleados[1] = new Empleado
            {
                Area = "Administracion Tesoreria",
                Nombre = "Angelica Betancourt",
                Cedula = "7.845.256.666",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-2",
                CargoId = 2, // ROL Administracion Tesoreria
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            Empleados[2] = new Empleado
            {
                Area = "Administracion Contraloria",
                Nombre = "Daniel Sanchez",
                Cedula = "8.845.256.667",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3, // ROL Administracion Contabilidad
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };


            return (Empleados.Where(e => e.Cedula == cedula).FirstOrDefault());
        }


        public List<Motivo> GetListMotivos(int CentroCostoId)
        {
            var wListMotivos = new List<Motivo>();
            if (CentroCostoId == 1)
            {
                var wM1 = new Motivo
                {
                    Id = 1,
                    Nombre = "Motivo 1 centro Operaciones 1"
                };
                var wM2 = new Motivo
                {
                    Id = 2,
                    Nombre = "Motivo 2 centro Operaciones 1"
                };
                wListMotivos.Add(wM1);
                wListMotivos.Add(wM2);
            }
            else
            {
                var wM1 = new Motivo
                {
                    Id = 1,
                    Nombre = "Motivo 1 centro Operaciones 2"
                };
                var wM2 = new Motivo
                {
                    Id = 2,
                    Nombre = "Motivo 2 centro Operaciones 2"
                };
                wListMotivos.Add(wM1);
                wListMotivos.Add(wM2);

            }

            return wListMotivos;
        }

        public List<Empleado> EmpleadoAll()
        {
            var LisEmpleado = new List<Empleado>();

            var Empleado1 = new Empleado
            {
                Area = "Empleado",
                Nombre = "Eliezer Vargas",
                Cedula = "6.845.256.665",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100",
                CargoId = 1, // ROL Empleado
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            var Empleado2 = new Empleado
            {
                Area = "Administracion Tesoreria",
                Nombre = "Angelica Betancourt",
                Cedula = "7.845.256.666",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-2",
                CargoId = 2, // ROL Administracion Tesoreria
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            var Empleado3 = new Empleado
            {
                Area = "Administracion Contraloria",
                Nombre = "Daniel Sanchez",
                Cedula = "8.845.256.667",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3, // ROL Administracion Contabilidad
                CentroOperaciones = "1",
                CentroCostos = "1",
                UnidadNegocio = "1"
            };

            LisEmpleado.Add(Empleado1);
            LisEmpleado.Add(Empleado2);
            LisEmpleado.Add(Empleado3);

            return LisEmpleado;

        }

    }
}
