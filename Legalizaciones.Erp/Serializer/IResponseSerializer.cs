using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Legalizaciones.Erp.Serializer
{
    class IResponseSerializer
    {
        #region BussniseUnit Method Interfaces

        Task<ListBussinesUnit> ListBussniseUnitsAsync<ListBussinesUnit>();
        Task<IEnumerable<XElement>> ListBussniseUnitsXMLAsync<UnidadNegocio>();
        Task<UnidadNegocio> GetBussniseUnitAsync<UnidadNegocio>(string IdBussniseUnit);
        Task<XElement> GetBussniseUnitXMLAsync<UnidadNegocio>(string IdBussniseUnit);

        #endregion

        #region OperationCenter Method Interfaces

        Task<ListOperationCenter> ListOperationCenterAsync<ListOperationCenter>();
        Task<IEnumerable<XElement>> ListOperationCenterXMLAsync<OperationCenter>();
        Task<OperationCenter> GetOperationCenterAsync<OperationCenter>(string IdOperationCenter);
        Task<XElement> GetOperationCenterXMLAsync<OperationCenter>(string IdOperationCenter);

        #endregion

        #region TaxtypesServices Method Interfaces

        Task<ListTaxtypesServices> ListTaxtypesServicesAsync<ListTaxtypesServices>();
        Task<IEnumerable<XElement>> ListTaxtypesServicesXMLAsync<ListTaxtypesServices>();
        Task<TaxtypesServices> GetTaxtypesServicesAsync<TaxtypesServices>(string IdTaxtypesServices);
        Task<XElement> GetTaxtypesServicesXMLAsync<TaxtypesServices>(string IdTaxtypesServices);

        #endregion

        #region Suppliers Method Interfaces

        Task<ListSuppliersAsync> ListSuppliersAsync<ListSuppliersAsync>();
        Task<IEnumerable<XElement>> ListSuppliersXMLAsync<UnidadNegocio>();
        Task<UnidadNegocio> GetSuppliersAsync<UnidadNegocio>(string IdSuppliers);
        Task<XElement> GetSuppliersXMLAsync<UnidadNegocio>(string IdSuppliers);

        #endregion

        #region ServicesTypes Method Interfaces

        Task<ListServicesTypes> ListServicesTypesAsync<ListServicesTypes>();
        Task<IEnumerable<XElement>> ListServicesTypesXMLAsync<UnidadNegocio>();
        Task<UnidadNegocio> GetServicesTypesAsync<UnidadNegocio>(string IdServicesTypes);
        Task<XElement> GetServicesTypesXMLAsync<UnidadNegocio>(string IdServicesTypes);

        #endregion

        #region CostCenters Method Interfaces

        Task<ListCostCenters> ListCostCentersAsync<ListCostCenters>();
        Task<IEnumerable<XElement>> ListCostCentersXMLAsync<UnidadNegocio>();
        Task<UnidadNegocio> GetCostCentersAsync<UnidadNegocio>(string IdCostCenters);
        Task<XElement> GetCostCentersXMLAsync<UnidadNegocio>(string IdCostCenters);

        #endregion
    }
}
