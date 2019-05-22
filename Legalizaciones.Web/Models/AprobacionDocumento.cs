using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models
{
    public class AprobacionDocumento
    {
        public string TipoSeleccionado { get; set; }
        public List<string> TipoSolicitud { get; set; } 
        public List <DataAprobacion> Aprobadores { get; set; } 
    }

    public class DataAprobacion
    {
        public string NombreAprobador { get; set; }
        public string CedulaAprobador { get; set; }
        public string EmailAprobador { get; set; }
        public int  Orden { get; set; }
        public int FlujoSolicitudId { get; set; }
        public int Estatus { get; set; }
        public string Descripcion { get; set; }
    }
}
