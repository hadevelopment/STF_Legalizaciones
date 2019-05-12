using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("Solicitud")]
    public class Solicitud : BaseModel
    {
        public Solicitud()
        {
            SolicitudGastos = new List<SolicitudGastos>();
        }
        public string NumeroSolicitud { get; set; }

        [ForeignKey("TipoSolicitud")]
        public int TipoSolicitudID { get; set; }
        public TipoSolicitud TipoSolicitud { get; set; }

        [Required(ErrorMessage = "Concepto es requerido.")]
        public string Concepto { get; set; }

        [Required(ErrorMessage = "Destino es requerido.")]
        [ForeignKey("Destino")]
        public int? DestinoID { get; set; }
        public Destino Destino { get; set; }

        [Required(ErrorMessage = "Estado Solicitud.")]
        [ForeignKey("EstadoID")]
        public int? EstadoID { get; set; }

        [Required(ErrorMessage = "Ciudad es requerido.")]
        [ForeignKey("Zona")]
        public int? ZonaID { get; set; }
        public Zona Zona { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Operaciones.")]
        public int CentroOperacionId { get; set; }

        [NotMapped]
        public CentroOperacion CentroOperacion { get; set; }

        [Required(ErrorMessage = "Seleccione una Unidad de Negocio.")]
        [DisplayName("Unidad Negocio")]
        public int UnidadNegocioId { get; set; }

        [NotMapped]
        public UnidadNegocio UnidadNegocio { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Costo.")]
        [DisplayName("Centro Costo")]
        public int CentroCostoId { get; set; }

        [NotMapped]
        public CentroCosto CentroCosto { get; set; }

        [Required(ErrorMessage = "Debe indicar Fecha Desde.")]
        [DisplayName("Fecha Desde")]
        public DateTime FechaDesde { get; set; }

        [Required(ErrorMessage = "Debe indicar Fecha Hasta.")]
        [DisplayName("Fecha Hasta")]
        public DateTime FechaHasta { get; set; }

        [Required(ErrorMessage = "El campo Concepto es requerido.")]
        [DisplayName("Moneda")]
        [ForeignKey("Moneda")]
        public int? MonedaId { get; set; }
        public Moneda Moneda { get; set; }

        [Required(ErrorMessage = "Estado de la Solicitud.")]
        [ForeignKey("EstadoSolicitud")]
        public int? EstadoId { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; }

        [NotMapped]
        public string GastosJSON { get; set; }

        public string EmpleadoCedula { get; set; }

        [NotMapped]
        public Empleado Empleado { get; set; }

        [DisplayName("Monto")]
        public float Monto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaSolicitud { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVencimiento { get; set; }

        public string RutaArchivo { get; set; }

        [NotMapped]
        public ICollection<SolicitudGastos> SolicitudGastos { get; set; }

        [NotMapped]
        public IFormFile Carta { get; set; }

        public string Extracto { get; set; }

        public string Banco { get; set; }

    }
}
