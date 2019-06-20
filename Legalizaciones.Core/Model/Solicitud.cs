using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using Legalizaciones.Model.Workflow;
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

        [Required(ErrorMessage = "Zona es requerida.")]
        [ForeignKey("Zona")]
        public int? ZonaID { get; set; }
        public Zona Zona { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Operaciones.")]
        public int? CentroOperacionId { get; set; }
        public string CentroOperacion { get; set; }

        [NotMapped]
        public CentroOperacion CentroOperacionObj { get; set; }

        [Required(ErrorMessage = "Seleccione una Unidad de Negocio.")]
        [DisplayName("Unidad Negocio")]
        public int? UnidadNegocioId { get; set; }
        public string UnidadNegocio { get; set; }

        [NotMapped]
        public UnidadNegocio UnidadNegocioObj { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Costo.")]
        [DisplayName("Centro Costo")]
        public int? CentroCostoId { get; set; }
        public string CentroCosto { get; set; }

        [NotMapped]
        public CentroCosto CentroCostoObj { get; set; }

        [Required(ErrorMessage = "Debe indicar Fecha Desde.")]
        [DisplayName("Fecha Desde")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaDesde { get; set; }

        [Required(ErrorMessage = "Debe indicar Fecha Hasta.")]
        [DisplayName("Fecha Hasta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaHasta { get; set; }


        [Required(ErrorMessage = "El campo Concepto es requerido.")]
        [DisplayName("Moneda")]
        [ForeignKey("Moneda")]
        public int? MonedaId { get; set; }
        public Moneda Moneda { get; set; }

        [ForeignKey("EstadoSolicitud")]
        public int? EstadoId { get; set; }
        public EstadoSolicitud EstadoSolicitud { get; set; }

        [ForeignKey("FlujoSolicitud")]
        public int? FlujoSolicitudId { get; set; }
        public FlujoSolicitud FlujoSolicitud { get; set; }

        [ForeignKey("PasoFlujoSolicitud")]
        public int? PasoFlujoSolicitudId { get; set; }
        public PasoFlujoSolicitud PasoFlujoSolicitud { get; set; }

        [NotMapped]
        public string GastosJSON { get; set; }

        [Required(ErrorMessage = "Debe indicar cedula del empleado.")]
        public string EmpleadoCedula { get; set; }

        [NotMapped]
        public Empleado Empleado { get; set; }

        [DisplayName("Monto")]
        [Range(0.001, float.MaxValue)]
        [Required(ErrorMessage = "Debe indicar un Monto valido.")]
        public decimal Monto { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaSolicitud { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaVencimiento { get; set; }

        public string RutaArchivo { get; set; }
        public string DocumentoERP { get; set; }
        public string ConsecutivoERP { get; set; }
        public string Area { get; set; }

        [NotMapped]
        public ICollection<SolicitudGastos> SolicitudGastos { get; set; }

        [NotMapped]
        public IFormFile Archivo { get; set; }

        [NotMapped]
        public List<Flujo> ListaFlujo { get; set; }

        public string Extracto { get; set; }

        public string Banco { get; set; }


        [NotMapped]
        public String AuxFechaDesde { get; set; }

        [NotMapped]
        public string AuxFechaHasta { get; set; }

        [NotMapped]
        public string EmailAprobador { get; set; }

    }
}
