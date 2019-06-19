using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Legalizaciones.Erp
{
    public class BaseSerializer
    {
        private string ConexionName;
        private string IdCia;
        private string IdRequest;
        private string IdProvider;
        private string User;
        private string Password;



        public XElement xmlRequest;
        public Dictionary<string, string> requestTypeCollection = new Dictionary<string, string>();

        public BaseSerializer()
        {
            SetSchemaConfigurationValues();
            requestTypeCollection.Add("WS_UNIDADES_NEGOCIO", "idUnidadNegocio");//BussinesUnit
            requestTypeCollection.Add("WS_CENTROS_OPERACION", "idCentroOperacion");//OperationCenter
            requestTypeCollection.Add("WS_IMPUESTO_TIPOS_SERVICIOS", "idTipoServicio");//TaxType
            requestTypeCollection.Add("WS_PROVEEDORES", "idProveedor");//Suppliers
            requestTypeCollection.Add("WS_TIPOS_SERVICIOS", "idTipoServicio");//ServiceType
            requestTypeCollection.Add("WS_CENTROS_COSTOS", "idCentroCosto");//CostCenter
        }

        public void SetSchemaConfigurationValues(string conexionName = "Unoee_STFCOL_Iva19", string idCia = "1", string idProvider = "STUDIOF", string user = "importunoee", string password = "studiof")
        {
            ConexionName = conexionName;
            IdCia = idCia;
            IdProvider = idProvider;
            User = user;
            Password = password;
        }

        public void SetRequestSchema(string requestType)
        {
            IdRequest = requestType;
            xmlRequest = new XElement("Consulta",
             new XElement("NombreConexion", ConexionName),
             new XElement("IdCia", IdCia),
             new XElement("IdProveedor", IdProvider),
             new XElement("IdConsulta", IdRequest),
             new XElement("Usuario", User),
             new XElement("Clave", Password),
             new XElement("Parametros",null)
            );
            xmlRequest.ToString();
        }

        public void BuildRequest(string requestType, string paramterRequestType = "")
        {
            SetRequestSchema(requestType);
            if (!String.IsNullOrEmpty(paramterRequestType))
            {
                SetParamterValue(paramterRequestType);
            }
        }

        public void SetParamterValue(string paramterRequestTypeValue)
        {
            var paramterRequestType = "";
            requestTypeCollection.TryGetValue(IdRequest, out paramterRequestType);
            xmlRequest.LastNode.ReplaceWith(new XElement("Parametros", new XElement(paramterRequestType, paramterRequestTypeValue)));
        }
    }
}
