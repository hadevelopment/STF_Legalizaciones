using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Legalizaciones.Model.ERP
{
    //WS_CENTROS_OPERACION
    [XmlRoot(ElementName = "Resultado")]
    public class OperationCenter
    {
        [XmlElement(ElementName = "idCentroOperacion")]
        public string IdCentroOperacion{ get; set; }

        [XmlElement(ElementName = "Nombre")]
        public string Nombre { get; set; }

        [XmlElement(ElementName = "Estatus")]
        public string Estatus { get; set; }

        [XmlElement(ElementName = "Regional")]
        public string Regional { get; set; }

        [XmlElement(ElementName = "Nombre1")]
        public string Nombre1{ get; set; }

        [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
        public string Id { get; set; }
    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class ListOperationCenter
    {
        [XmlElement(ElementName = "Resultado")]
        public List<OperationCenter> Resultado { get; set; }
    }
}
