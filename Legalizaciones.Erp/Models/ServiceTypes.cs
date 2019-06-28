using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Legalizaciones.Erp.Models
{
    //WS_TIPOS_SERVICIOS
    [XmlRoot(ElementName = "Resultado")]
    public class ServiceTypes
    {
        [XmlElement(ElementName = "idTipoServicio")]
        public string IdTipoServicio { get; set; }

        [XmlElement(ElementName = "Nombre")]
        public string Nombre { get; set; }

        [XmlElement(ElementName = "Contabilizacion_x0020_costo")]
        public string Contabilizacion_X0020_Costo { get; set; }

        [XmlElement(ElementName = "Clase")]
        public string Clase { get; set; }

        [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class ListServiceTypes
    {
        [XmlElement(ElementName = "Resultado")]
        public List<ServiceTypes> Resultado { get; set; }
    }
}
