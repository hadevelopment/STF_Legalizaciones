using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Legalizaciones.Erp.Models
{
    //WS_CENTROS_COSTOS
    [XmlRoot(ElementName = "Resultado")]
    public class CostCenters
    {
        [XmlElement(ElementName = "idCentroCosto")]
        public string IdCentroCosto { get; set; }

        [XmlElement(ElementName = "Nombre")]
        public string Nombre { get; set; }

        [XmlElement(ElementName = "Estatus")]
        public string Estatus { get; set; }
    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class ListCostCenters
    {
        [XmlElement(ElementName = "Resultado")]
        public List<ListCostCenters> Resultado { get; set; }
    }
}
