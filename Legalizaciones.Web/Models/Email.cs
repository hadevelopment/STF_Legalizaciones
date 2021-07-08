using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models
{
    public class Email
    {
        public string NombreDestinatario { get; set; }
        public string NumeroDocumento { get; set; }
        public string Direccion { get; set; }
        public string Fecha { get; set; }
    }
}
