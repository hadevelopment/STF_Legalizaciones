using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Helpers
{
    public class GastosJSON
    {

        public string Zona { get; set; }
        public int Proveedor { get; set; }
        public int Servicio { get; set; }
        public string Descripcion { get; set; }
        public int Origen { get; set; }
        public int Destino { get; set; }
        public int Cantidad { get; set; }
        public float Valor { get; set; }
        public float Monto { get; set; }

    }
}
