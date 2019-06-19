using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Erp;
using Legalizaciones.Erp.Models;
using Newtonsoft.Json;

namespace Legalizaciones.Web.Engine
{
    public class ErpMethod:ResponseSerializer
    {
        //var json = JsonConvert.SerializeObject(unidadNegocio);
        public async Task<ListBussinesUnit> UnidadNegocioCollectionAsync()
        {
            ListBussinesUnit unidadNegocio = await ListBussniseUnitsAsync<ListBussinesUnit>();
            return unidadNegocio;
        }

        public async Task<BusinessUnit> UnidadNegocioSingleAsync(string id)
        {
            BusinessUnit unidadNegocio = await GetBussinesUnitAsync<BusinessUnit>(id);
            return unidadNegocio;
        }

        public async Task<ListOperationCenter> CentroOperacionesCollectionAsync()
        {
            ListOperationCenter centroOperaciones = await ListOperationCenterAsync<ListOperationCenter>();
            return centroOperaciones;
        }

        public async Task<OperationCenter> CentroOperacionesSingleAsync(string id)
        {
            OperationCenter centroOperaciones = await GetOperationCenterAsync<OperationCenter>(id);
            return centroOperaciones;
        }

        public async Task<TaxTypes> TipoImpuestosSingleAsync(string id)
        {
            TaxTypes tipoImpuesto = await GetTaxtypesServicesAsync<TaxTypes>(id);
            return tipoImpuesto;
        }

        public async Task<ListSuppliers> ProveedoresCollectionAsync()
        {
            ListSuppliers proveedores = await ListSuppliersAsync<ListSuppliers>();
            return proveedores;
        }
        public async Task<Suppliers> ProveedoresSingleAsync(string id)
        {
            Suppliers proveedores = await GetSuppliersAsync<Suppliers>(id);
            return proveedores;
        }

        public async Task<ListServiceTypes> TiposServiciosCollectionAsync()
        {
            ListServiceTypes tipoServicios = await ListServicesTypesAsync<ListServiceTypes>();
            return tipoServicios;
        }
        public async Task<ServiceTypes> TipoServiciosSingleAsync(string id)
        {
            ServiceTypes tipoServicios = await GetServicesTypesAsync<ServiceTypes>(id);
            return tipoServicios;
        }
        public async Task<ListCostCenters> CentroCostosCollectionAsync()
        {
            ListCostCenters centroCosto = await ListCostCentersAsync<ListCostCenters>();
            return centroCosto;
        }
        public async Task<CostCenters> CentroCostosSingleAsync(string id)
        {
            CostCenters centroCosto = await GetCostCentersAsync<CostCenters>(id);
            return centroCosto;
        }
    }
}
