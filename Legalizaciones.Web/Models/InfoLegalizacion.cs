using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models
{
    public class InfoLegalizacion
    {
        public int Id { get; set; }
        public string NumeroSolicitud { get; set; }
        public string FechaCreacion { get; set; }
        public string Concepto { get; set; }
        public double Monto { get; set; }
        public string Moneda { get; set; }
        public double Tasa { get; set; }//RATA CAMBIO
        public string Beneficiario { get; set; }//EMPLEADO
        public string EmpleadoCedula { get; set; }
        public string Estado { get; set; }//SATATUS SOLICITUD
        public int IdDocErp { get; set; }
        public string  ConsecutivoErp { get; set; }
        public string FechaEntrega { get; set; }
        public string FechaVencimiento { get; set; }
        public int DiasTrascurridos { get; set; }
        public string RutaArchivo { get; set; }
    }
}
