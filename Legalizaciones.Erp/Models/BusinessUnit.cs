using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Legalizaciones.Erp.Models
{
    //WS_UNIDADES_NEGOCIO
    [XmlRoot(ElementName = "Resultado")]
    public class BusinessUnit
    {
            [XmlElement(ElementName = "idUnidadNegocio")]
            public string IdUnidadNegocio { get; set; }

            [XmlElement(ElementName = "Nombre")]
            public string Nombre { get; set; }

            [XmlElement(ElementName = "Estatus")]
            public string Estatus { get; set; }

            [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
            public string Id { get; set; }

    }

    [XmlRoot(ElementName = "NewDataSet")]
    public class ListBussinesUnit
    {
        [XmlElement(ElementName = "Resultado")]
        public List<BusinessUnit> Resultado { get; set; }
    }

}
