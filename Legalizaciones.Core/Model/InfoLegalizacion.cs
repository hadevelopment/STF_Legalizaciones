using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Model
{
    public class InfoLegalizacion
    {
        public int legalizacionID { get; set; }
        public int Id { get; set; }
        public string NumeroSolicitud { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaCreacion { get; set; }
        public string Concepto { get; set; }
        public string Monto { get; set; }
        public string Moneda { get; set; }
        public string Tasa { get; set; }//RATA CAMBIO
        public string Beneficiario { get; set; }//EMPLEADO
        public string EmpleadoCedula { get; set; }
        public int EstadoId { get; set; }//STATUS SOLICITUD
        public string Estado { get; set; }//STATUS LEGALIZACION NOMBRE
        public string EstadoLegalizacion { get; set; }
        public string IdDocErp { get; set; }
        public string  ConsecutivoErp { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaEntrega { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVencimiento { get; set; }
        public int DiasTrascurridos { get; set; }
        public string RutaArchivo { get; set; }
        public string Accion { get; set; }
    }
}
