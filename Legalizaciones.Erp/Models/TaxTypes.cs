using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Legalizaciones.Erp.Models
{
    //WS_IMPUESTO_TIPOS_SERVICIOS
    [XmlRoot(ElementName = "Resultado")]
    public class TaxTypes
    {
        [XmlElement(ElementName = "idImpuesto")]
        public string IdImpuesto { get; set; }
        [XmlElement(ElementName = "nombre")]
        public string Nombre { get; set; }
        [XmlElement(ElementName = "valor")]
        public string Valor { get; set; }
        [XmlElement(ElementName = "estatus")]
        public string Estatus { get; set; }

        [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class ListTaxTypes
    {
        [XmlElement(ElementName = "Resultado")]
        public List<ListTaxTypes> Resultado { get; set; }
    }
}
