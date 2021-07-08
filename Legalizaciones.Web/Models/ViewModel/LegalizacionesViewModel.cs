using Legalizaciones.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models.ViewModel
{

    /// <summary>
    /// clase viewmodel para las legalizaciones que interactua entre la vista y el modelo de la bd
    /// </summary>
    public class LegalizacionesViewModel
    {

        //Legalizaciones ID
        [DisplayName("legalizacionesId")]
        public int legalizacionesId { get; set; }

        //Area de Solicitud de Anticipo
        [DisplayName("ID")]
        public int AnticipoId { get; set; }

        [DisplayName("ID Documento en ERP")]
        public int DocumentoERPID { get; set; }

        [DisplayName("Registro")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Vencimiento")]
        public DateTime FechaVencimiento { get; set; }

        [DisplayName("Concepto")]
        public string Concepto { get; set; }

        [DisplayName("Valor")]
        public decimal Monto { get; set; }

        [DisplayName("Centro de Costo")]
        public int CentroCosto { get; set; }

        public string CentroCostoDescripcion { get; set; }

        public SelectList ListaCentroCosto { get; set; }


        [DisplayName("Fecha desde")]
        public DateTime FechaDesde { get; set; }

        [DisplayName("Fecha hasta")]
        public DateTime FechaHasta { get; set; }


        //area del Empleado
        [DisplayName("Beneficiario del Anticipo")]
        public string Nombre { get; set; }

        [DisplayName("Cédula de Ciudadanía")]
        public string Cedula { get; set; }

        [DisplayName("Área")]
        public string Area { get; set; }

        [DisplayName("Cargo")]
        public string Cargo { get; set; }


        //area de legalizacion
        [DisplayName("Recibo de Caja")]
        public long ReciboCaja { get; set; }

        [DisplayName("Consignación")]
        public long Consignacion { get; set; }

        [DisplayName("Valor Consignación")]
        public string Valor { get; set; }


        [DisplayName("Banco")]
        public int BancoId { get; set; }
        public SelectList ListaBanco { get; set; }


        [DisplayName("Moneda")]
        public int? MonedaId { get; set; }
        public SelectList ListaMoneda { get; set; }

        public string Moneda { get; set; }

        [DisplayName("Tasa")]
        public string ValorTasa { get; set; }

        //Para el registro de gastos
        [DisplayName("ID del item")]
        public int GastosId { get; set; }

        [DisplayName("Fecha del gasto")]
        public DateTime FechaGasto { get; set; }

        [DisplayName("Centro de operación")]

        public int CentroOperacion { get; set; }

        public SelectList ListaCentroOperacion { get; set; }


        [DisplayName("Unidad de negocio")]
        public int UnidadNegocio { get; set; }
        public SelectList ListaUnidadNegocio { get; set; }


        [DisplayName("Motivo")]
        public string MotivoId { get; set; }

        public SelectList ListaMotivo { get; set; }

        [DisplayName("País")]
        public int PaisId { get; set; }

        [DisplayName("Ciudad")]
        public int CiudadId { get; set; }

        [DisplayName("Tio de servicio")]
        public int TiposervicioId { get; set; }

        [DisplayName("Proveedor")]
        public int ProveedorId { get; set; }

        [DisplayName("Concepto del gasto")]
        public string ConceptoGasto { get; set; }

        [DisplayName("Valor del Gasto")]
        public decimal MontoGasto { get; set; }

        [DisplayName("Origen")]
        public string Origen { get; set; }

        [DisplayName("Destino")]
        public string Destino { get; set; }


        //Para que me carge los gastos en el grid
        public ICollection<SolicitudGastos> SolicitudGastos { get; set; }

        //para la subida de archivo
        public IFormFile Soporte { get; set; }

        public string GastosJSON { get; set; }


        //campos para el formulario de envio
        [DisplayName("Zonas visitadas")]
        public string ZonasVisitadas { get; set; }

        [DisplayName("Objetivo")]
        public string Objetivo { get; set; }

        [DisplayName("Actividades desarrolladas")]
        public string ActividadesDesarrolladas { get; set; }

        [DisplayName("Resultados obtenidos")]
        public string ResultadosObtenidos { get; set; }

        [DisplayName("Compromisos")]
        public string Compromisos { get; set; }

        //para a legalizacion de gastos sin anticipo
        public SelectList ListaEmpleado { get; set; }

        [DisplayName("Beneficiario")]
        public string CedulaId { get; set; }
        public int ConAnticipo { get; set; }

        [NotMapped] public List<LegalizacionGastos> LegalizacionGastos { get; set; }


    }
}
