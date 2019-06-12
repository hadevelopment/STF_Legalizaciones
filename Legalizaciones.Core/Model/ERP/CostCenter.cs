using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Legalizaciones.Model.ERP
{
    //WS_CENTROS_COSTOS
    [XmlRoot(ElementName = "Resultado")]
    public class CostCenter
    {
        [XmlElement(ElementName = "idCentroCosto")]
        public string IdCentroCosto { get; set; }

        [XmlElement(ElementName = "Nombre")]
        public string Nombre { get; set; }

        [XmlElement(ElementName = "Estatus")]
        public string Estatus { get; set; }
    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class ListCostCenter
    {
        [XmlElement(ElementName = "Resultado")]
        public List<ListCostCenter> Resultado { get; set; }
    }
}
