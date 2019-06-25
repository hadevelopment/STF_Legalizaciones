using Legalizaciones.Erp.Serializer;
using Legalizaciones.Erp.Models;
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
    public class ResponseSerializer : IResponseSerializer
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

        public async Task<Models.BusinessUnit> GetBussinesUnitAsync<BusinessUnit>(string IdBussniseUnit)
        {
            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO", IdBussniseUnit);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
            var d = ReadXmlString<Models.BusinessUnit>(s);
            return d;
        }

        public async Task<Models.ListBussinesUnit> ListBussniseUnitsAsync<ListBussinesUnit>()
        {

            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO");
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<Models.ListBussinesUnit>(s);

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

        public async Task<XElement> GetBussniseUnitXMLAsync<BusinessUnit>(string IdBussniseUnit)
        {
            BaseSerializer.BuildRequest("WS_UNIDADES_NEGOCIO", IdBussniseUnit);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();
            return s;
        }

        public async Task<Models.ListOperationCenter> ListOperationCenterAsync<ListOperationCenter>()
        {
            BaseSerializer.BuildRequest("WS_CENTROS_OPERACION");
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<Models.ListOperationCenter>(s);
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

        public async Task<Models.OperationCenter> GetOperationCenterAsync<OperationCenter>(string IdOperationCenter)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_OPERACION", IdOperationCenter);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
            var d = ReadXmlString<Models.OperationCenter>(s);
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

        public async Task<Models.ListTaxTypes> ListTaxtypesServicesAsync<ListTaxTypes>()
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<Models.ListTaxTypes>(s);
            return d;
        }

        public async Task<IEnumerable<XElement>> ListTaxtypesServicesXMLAsync<ListTaxType>()
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<Models.TaxTypes> GetTaxtypesServicesAsync<TaxTypes>(string IdTaxtypesServices)
        {
            Models.TaxTypes d = new Models.TaxTypes();
            try
            {
                BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS", IdTaxtypesServices);
                var a = BaseSerializer.xmlRequest;
                var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
                var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
                 d = ReadXmlString<Models.TaxTypes>(s);
                return d;
            }
            catch(Exception x)
            {
                string n = x.ToString();
            }

            return d;
        }

        public async Task<XElement> GetTaxtypesServicesXMLAsync<TaxTypes>(string IdTaxtypesServices)
        {
            BaseSerializer.BuildRequest("WS_IMPUESTO_TIPOS_SERVICIOS", IdTaxtypesServices);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();
            return s;
        }

        public async Task<Models.ListSuppliers> ListSuppliersAsync<ListSuppliers>()
        {
            BaseSerializer.BuildRequest("WS_PROVEEDORES");
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<Models.ListSuppliers>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListSuppliersXMLAsync<Suppliers>()
        {
            BaseSerializer.BuildRequest("WS_PROVEEDORES");
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<Models.Suppliers> GetSuppliersAsync<Suppliers>(string IdSuppliers)
        {

            BaseSerializer.BuildRequest("WS_PROVEEDORES", IdSuppliers);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
            var d = ReadXmlString<Models.Suppliers>(s);
            return d;
        }

        public async Task<XElement> GetSuppliersXMLAsync<Suppliers>(string IdSuppliers)
        {
            BaseSerializer.BuildRequest("WS_PROVEEDORES", IdSuppliers);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<Models.ListServiceTypes> ListServicesTypesAsync<ListServicesTypes>()
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<Models.ListServiceTypes>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListServicesTypesXMLAsync<ServiceTypes>()
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<Models.ServiceTypes> GetServicesTypesAsync<ServiceTypes>(string IdServicesTypes)
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS", IdServicesTypes);
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
            var d = ReadXmlString<Models.ServiceTypes>(s);
            return d;
        }

        public async Task<XElement> GetServicesTypesXMLAsync<ServiceTypes>(string IdServicesTypes)
        {
            BaseSerializer.BuildRequest("WS_TIPOS_SERVICIOS", IdServicesTypes);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }

        public async Task<Models.ListCostCenters> ListCostCentersAsync<ListCostCenters>()
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].FirstNode.ToString();
            var d = ReadXmlString<Models.ListCostCenters>(s);

            return d;
        }

        public async Task<IEnumerable<XElement>> ListCostCentersXMLAsync<CostCenters>()
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS");
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants();

            return s;
        }

        public async Task<Models.CostCenters> GetCostCentersAsync<CostCenters>(string IdCostCenters)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS");
            var a = BaseSerializer.xmlRequest;
            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault().ToString();
            var d = ReadXmlString<Models.CostCenters>(s);
            return d;
        }

        public async Task<XElement> GetCostCentersXMLAsync<CostCenters>(string IdCostCenters)
        {
            BaseSerializer.BuildRequest("WS_CENTROS_COSTOS", IdCostCenters);
            var a = BaseSerializer.xmlRequest;

            var r = await WSUNOEE.EjecutarConsultaXMLAsync(BaseSerializer.xmlRequest.ToString());
            var s = r.Nodes[1].Elements().FirstOrDefault().Descendants().FirstOrDefault();

            return s;
        }
    }
}
