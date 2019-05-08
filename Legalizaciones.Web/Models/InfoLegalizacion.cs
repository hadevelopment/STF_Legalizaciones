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
        public DateTime FechaCreacion { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }
        public string Moneda { get; set; }
        public string Tasa { get; set; }//RATA CAMBIO
        public string Beneficiario { get; set; }//EMPLEADO
        public string Estado { get; set; }//SATATUS SOLICITUD
        public int IdDocErp { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int DiasTrascurridos { get; set; }
    }
}
