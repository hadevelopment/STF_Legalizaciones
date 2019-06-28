using Legalizaciones.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        public string DocumentoERPID { get; set; }

        [DisplayName("Registro")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaRegistro { get; set; }

        [DisplayName("Vencimiento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVencimiento { get; set; }

        [DisplayName("Concepto")]
        public string Concepto { get; set; }

        [DisplayName("Valor")]
        public decimal Monto { get; set; }

        [DisplayName("Centro de Costo")]
        public int? CentroCosto { get; set; }

        public string CentroCostoDescripcion { get; set; }

        public SelectList ListaCentroCosto { get; set; }


        [DisplayName("Fecha desde")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaDesde { get; set; }

        [DisplayName("Fecha hasta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaHasta { get; set; }

        //Objeto Empleado
        public KactusEmpleado Empleado { get; set; }

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

        [DisplayName("Fecha del gasto")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaGasto { get; set; }

        [DisplayName("Centro de operación")]

        public int? CentroOperacion { get; set; }

        public SelectList ListaCentroOperacion { get; set; }


        [DisplayName("Unidad de negocio")]
        public int? UnidadNegocio { get; set; }
        public SelectList ListaUnidadNegocio { get; set; }


        [DisplayName("Motivo")]
        public string MotivoId { get; set; }

        public SelectList ListaMotivo { get; set; }

        [DisplayName("País")]
        public int PaisId { get; set; }

        [DisplayName("Ciudad")]
        public int CiudadId { get; set; }

        [DisplayName("Tipo de servicio")]
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

        public int DestinoID{ get; set; }


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

        public decimal MontoAnticipoEntregado { get; set; }

        public decimal MontoGastosReportados { get; set; }

        public decimal MontoSaldo { get; set; }

        public int ConAnticipo { get; set; }

        [NotMapped] public List<LegalizacionGastos> LegalizacionGastos { get; set; }


    }
}
