using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Legalizaciones.Erp.Serializer
{
    public interface IResponseSerializer 
    {
        #region BussniseUnit Method Interfaces

        Task<Models.BusinessUnit> GetBussinesUnitAsync<BusinessUnit>(string IdBussniseUnit);
        Task<Models.ListBussinesUnit> ListBussniseUnitsAsync<ListBussinesUnit>();
        Task<IEnumerable<XElement>> ListBussniseUnitsXMLAsync<ListBussinesUnit>();
        Task<XElement> GetBussniseUnitXMLAsync<BusinessUnit>(string IdBussniseUnit);

        #endregion

        #region OperationCenter Method Interfaces

        Task<Models.ListOperationCenter> ListOperationCenterAsync<ListOperationCenter>();
        Task<IEnumerable<XElement>> ListOperationCenterXMLAsync<ListOperationCenter>();
        Task<Models.OperationCenter> GetOperationCenterAsync<OperationCenter>(string IdOperationCenter);
        Task<XElement> GetOperationCenterXMLAsync<OperationCenter>(string IdOperationCenter);

        #endregion

        #region TaxtypesServices Method Interfaces

        Task<Models.ListTaxType> ListTaxtypesServicesAsync<ListTaxType>();
        Task<IEnumerable<XElement>> ListTaxtypesServicesXMLAsync<ListTaxType>();
        Task<Models.TaxTypes> GetTaxtypesServicesAsync<TaxTypes>(string IdTaxtypesServices);
        Task<XElement> GetTaxtypesServicesXMLAsync<TaxTypes>(string IdTaxtypesServices);

        #endregion

        #region Suppliers Method Interfaces

        Task<Models.ListSuppliers> ListSuppliersAsync<ListSuppliers>();
        Task<IEnumerable<XElement>> ListSuppliersXMLAsync<Suppliers>();
        Task<Models.Suppliers> GetSuppliersAsync<Suppliers>(string IdSuppliers);
        Task<XElement> GetSuppliersXMLAsync<Suppliers>(string IdSuppliers);

        #endregion

        #region ServicesTypes Method Interfaces

        Task<Models.ListServiceTypes> ListServicesTypesAsync<ListServicesTypes>();
        Task<IEnumerable<XElement>> ListServicesTypesXMLAsync<ServiceTypes>();
        Task<Models.ServiceTypes> GetServicesTypesAsync<ServiceTypes>(string IdServicesTypes);
        Task<XElement> GetServicesTypesXMLAsync<ServiceTypes>(string IdServicesTypes);

        #endregion

        #region CostCenters Method Interfaces

        Task<Models.ListCostCenters> ListCostCentersAsync<ListCostCenters>();
        Task<IEnumerable<XElement>> ListCostCentersXMLAsync<CostCenters>();
        Task<Models.CostCenters> GetCostCentersAsync<CostCenters>(string IdCostCenters);
        Task<XElement> GetCostCentersXMLAsync<CostCenters>(string IdCostCenters);

        #endregion
    }
}
