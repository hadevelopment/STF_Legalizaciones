﻿using Legalizaciones.Model;
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
            CentroCosto centroCosto = new CentroCosto();
            centroCosto.Id = 1;
            centroCosto.Nombre = "Centro de Costo";
            return centroCosto;
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
                Cedula = "8.845.256.667",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3 // ROL Administracion Contraloria
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
                Cedula = "8.845.256.667",
                Direccion = "Calle 28 No. 13A - 15. Piso 10",
                Ciudad = "Bogota",
                Telefono = "(1) 560 00100-3",
                CargoId = 3 // ROL Administracion Contraloria
            };


            return (Empleados[ind - 1]);
        }


    }
}
