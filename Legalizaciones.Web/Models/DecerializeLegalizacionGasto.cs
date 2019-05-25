using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models
{
    public class DecerializeLegalizacionGasto
    {
        public DateTime FechaGasto { get; set; }
        public int PaisId { get; set; }
        public int CiudadId { get; set; }
        public int TipoServicioId { get; set; }
        public int ProveedorId { get; set; }
        public string Concepto { get; set; }
        public int Item { get; set; }
        public string Fecha { get; set; }
        public string CentroOper { get; set; }
        public string UnidadNeg { get; set; }
        public string CentroCost { get; set; }
        public string Pais { get; set; }
        public string Ciudad { get; set; }
        public string Servicio { get; set; }
        public string Proveedor { get; set; }
        public string ConceptoGasto { get; set; }
        public string Acciones { get; set; }

    }

}
