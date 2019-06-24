using Legalizaciones.Interface;
using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Helpers
{
    public class UNOEE 
    {
        public UNOEE()
        {
        }

        public int Empleado_logeado()
        {
            // 1= ROL Empleado
            // 2= ROL Administracion Tesoreria
            // 3= ROL Administracion Contraloria

            int EL_Empleado = 2;
            return EL_Empleado;
        }

        public List<CentroCosto> getCentroCostos()
        {
            List<CentroCosto> Lista = new List<CentroCosto>();
            CentroCosto centroCosto = new CentroCosto();
            centroCosto.Id = 1;
            centroCosto.Nombre = "Centro de Costo";

            Lista.Add(centroCosto);
            return Lista;
        }

        public List<CentroOperacion> getCentroOperaciones()
        {
            List<CentroOperacion> Lista = new List<CentroOperacion>();
            CentroOperacion centroOperacion = new CentroOperacion();
            centroOperacion.Id = 1;
            centroOperacion.Nombre = "Centro de Operacion";

            Lista.Add(centroOperacion);
            return Lista;
        }


        public List<UnidadNegocio> getUnidadNegocios()
        {
            List<UnidadNegocio> Lista = new List<UnidadNegocio>();
            UnidadNegocio unidadNegocio = new UnidadNegocio();
            unidadNegocio.Id = 1;
            unidadNegocio.Nombre = "Unidad de Negocio";

            Lista.Add(unidadNegocio);
            return Lista;
        }


        public CentroCosto getCentroCosto(int? IdCentroCosto)
        {
            CentroCosto CentroCostos = new CentroCosto();
            CentroCostos.Id = 1;
            CentroCostos.Nombre = "Centro de Costo";
            return CentroCostos;
        }

        public CentroOperacion getCentroOperacion(int? IdCentroOperacion)
        {
            CentroOperacion centroOperacion = new CentroOperacion();
            centroOperacion.Id = 1;
            centroOperacion.Nombre = "Centro de Operacion";
            return centroOperacion;
        }


        public UnidadNegocio getUnidadNegocio(int? IdUnidadNegocio)
        {
            UnidadNegocio unidadNegocio = new UnidadNegocio();
            unidadNegocio.Id = 1;
            unidadNegocio.Nombre = "Unidad de Negocio";
            return unidadNegocio;
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

    }
}