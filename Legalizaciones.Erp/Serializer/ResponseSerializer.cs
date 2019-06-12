using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using UNOEWebService;

namespace Legalizaciones.Erp
{
    class ResponseSerializer
    {
        BaseSerializer BaseSerializer = new BaseSerializer();
        WSUNOEESoapClient WSUNOEE = new WSUNOEESoapClient(WSUNOEESoapClient.EndpointConfiguration.WSUNOEESoap);

        private static ObjectType ReadXmlString<ObjectType>(string xmlString)
        {
            using (var sw = new StringReader(xmlString))
            {
                return (ObjectType)new XmlSerializer(typeof(ObjectType)).Deserialize(sw);
            }
        }

        public async Task<UnidadNegocio> GetBussniseUnitAsync<UnidadNegocio>(string IdBussniseUnit)
        {

            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO", IdBussniseUnit);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
            var d = ReadXmlString<UnidadNegocio>(s);

            return d;
        }

        public async Task<ListBussinesUnit> ListBussniseUnitsAsync<ListBussinesUnit>()//1
        {

            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<ListBussinesUnit>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListBussniseUnitsXMLAsync<ListBussinesUnit>()
        {
            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<XElement> GetBussniseUnitXMLAsync<UnidadNegocio>(string IdBussniseUnit)
        {
            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO", IdBussniseUnit);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<ListOperationCenter> ListOperationCenterAsync<ListOperationCenter>()//2
        {
            BaseSerializer.BuildRequest("WS_CENTROS_OPERACION");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<ListOperationCenter>(s);

            return d;

        }

        public async Task<IEnumerable<XElement>> ListOperationCenterXMLAsync<ListOperationCenter>()
        {
            BaseSerializer.BuildRequest("WS_CENTROS_OPERACION");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<OperationCenter> GetOperationCenterAsync<OperationCenter>(string IdOperationCenter)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_OPERACION", IdOperationCenter);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<OperationCenter>(s);

            return d;
        }

        public async Task<XElement> GetOperationCenterXMLAsync<OperationCenter>(string IdOperationCenter)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_OPERACION", IdOperationCenter);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<ListTaxtypesServices> ListTaxtypesServicesAsync<ListTaxtypesServices>()
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<ListTaxtypesServices>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListTaxtypesServicesXMLAsync<ListTaxtypesServices>()
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<ListUnidadNegocio> GetTaxtypesServicesAsync<ListUnidadNegocio>(string IdTaxtypesServices)
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS", IdTaxtypesServices);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();

            var d = ReadXmlString<ListUnidadNegocio>(s);

            return d;
        }

        public async Task<XElement> GetTaxtypesServicesXMLAsync<UnidadNegocio>(string IdTaxtypesServices)
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS", IdTaxtypesServices);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<ListSuppliersAsync1> ListSuppliersAsync<ListSuppliersAsync1>()
        {
            BaseSerializer.BuildRequest("WS_PROVEEDORES");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<ListSuppliersAsync1>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListSuppliersXMLAsync<UnidadNegocio>()
        {
            BaseSerializer.BuildRequest("WS_PROVEEDORES");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }


        public async Task<UnidadNegocio> GetSuppliersAsync<UnidadNegocio>(string IdSuppliers)
        {

            BaseSerializer.BuildRequest("WS_PROVEEDORES", IdSuppliers);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<UnidadNegocio>(s);

            return d;
        }

        public async Task<XElement> GetSuppliersXMLAsync<UnidadNegocio>(string IdSuppliers)
        {
            BaseSerializer.BuildRequest("WS_PROVEEDORES", IdSuppliers);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<ListServicesTypes> ListServicesTypesAsync<ListServicesTypes>()
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<ListServicesTypes>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListServicesTypesXMLAsync<UnidadNegocio>()
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<UnidadNegocio> GetServicesTypesAsync<UnidadNegocio>(string IdServicesTypes)
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS", IdServicesTypes);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<UnidadNegocio>(s);

            return d;
        }

        public async Task<XElement> GetServicesTypesXMLAsync<UnidadNegocio>(string IdServicesTypes)
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS", IdServicesTypes);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<ListCostCenters> ListCostCentersAsync<ListCostCenters>()
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<ListCostCenters>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListCostCentersXMLAsync<UnidadNegocio>()
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<UnidadNegocio> GetCostCentersAsync<UnidadNegocio>(string IdCostCenters)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS", IdCostCenters);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<UnidadNegocio>(s);

            return d;
        }

        public async Task<XElement> GetCostCentersXMLAsync<UnidadNegocio>(string IdCostCenters)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS", IdCostCenters);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }
    }
}
