using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models.ViewModel
{
    public class SolicitudTDCViewModel
    {
        public string Cedula {get;set;}

        public string Extracto { get; set; }

        public string Banco { get; set; }

        public decimal Monto { get; set; }

        public int id { get; set; }
        
    }
}
