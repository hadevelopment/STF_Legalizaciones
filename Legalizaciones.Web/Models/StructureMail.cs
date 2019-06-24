using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models
{
    public class StructureMail
    {
        public StructureMail()
        {
            Destinatario = new List<string>();
        }

        public string Subject { get; set; }
        public string Body { get; set; }
        public string PathAdjunto { get; set; }
        public string NombreDestinatario { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string DatosStf{ get; set; }
        public string Fecha { get; set; }
        public List<string> Destinatario { get; set; }
        public string SolicitudId { get; set; }
        public string NombreEmpleado { get; set; }
        public string CedulaEmpleado { get; set; }
        public string CargoEmpleado { get; set; }
        public string Concepto { get; set; }
        public string Destino { get; set; }
        public string Zona { get; set; }
        public string Moneda { get; set; }
        public string Monto { get; set; }
    }
}
