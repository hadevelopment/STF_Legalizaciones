using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Legalizaciones.Model.ERP
{
    //WS_PROVEEDORES
    [XmlRoot(ElementName = "Resultado")]
    public class Suppliers
    {
        [XmlElement(ElementName = "idProveedor")]
        public string IdProveedor{ get; set; }

        [XmlElement(ElementName = "Nombre")]
        public string Nombre { get; set; }

        [XmlElement(ElementName = "telefono")]
        public string Telefono { get; set; }

        [XmlElement(ElementName = "celular")]
        public string Celular { get; set; }

        [XmlElement(ElementName = "FechaReg")]
        public string FechaReg { get; set; }

        [XmlElement(ElementName = "sucursal")]
        public string Sucursal { get; set; }

        [XmlElement(ElementName = "des_sucursal")]
        public string Des_Sucuursal { get; set; }

        [XmlElement(ElementName = "pais")]
        public string Pais { get; set; }

        [XmlElement(ElementName = "depto")]
        public string Depto { get; set; }

        [XmlElement(ElementName = "ciudad")]
        public string Ciudad { get; set; }

        [XmlElement(ElementName = "direccion")]
        public string Direccion { get; set; }

        [XmlElement(ElementName = "email")]
        public string Email { get; set; }

        [XmlAttribute(AttributeName = "id", Namespace = "urn:schemas-microsoft-com:xml-diffgram-v1")]
        public string Id { get; set; }
    }


    [XmlRoot(ElementName = "NewDataSet")]
    public class ListSuppliers
    {
        [XmlElement(ElementName = "Resultado")]
        public List<Suppliers> Resultado { get; set; }
    }

}
