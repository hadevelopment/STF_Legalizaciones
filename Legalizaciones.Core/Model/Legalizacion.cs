using System.Collections.Generic;
using Legalizaciones.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Legalizaciones.Model.Workflow;
using Legalizaciones.Model.ItemSolicitud;
using System.ComponentModel;
using System;
using Legalizaciones.Model.Jerarquia;

namespace Legalizaciones.Model
{
    [Table("Legalizacion")]
    public class Legalizacion : BaseModel
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Legalizacion()
        {
            //Solicitud          = new HashSet<Solicitud>();
            //SolicitudGastos    = new HashSet<SolicitudGastos>();
            //LegalizacionGastos = new HashSet<LegalizacionGastos>();
        }

        [ForeignKey("Solicitud")]
        public int SolicitudID { get; set; }

        public string EmpleadoCedula { get; set; }

        public string EmpleadoNombre { get; set; }

        [Required(ErrorMessage = "Recibo de caja es requerido.")]
        public long ReciboCaja { get; set; }

        [Required(ErrorMessage = "Consignacion es requerido.")]
        public long Consignacion { get; set; }

        [Required(ErrorMessage = "Recibo de caja es requerido.")]
        public string Valor { get; set; }

        [ForeignKey("Banco")]
        public int BancoId { get; set; }

        [ForeignKey("FlujoSolicitud")]
        public int? FlujoSolicitudId { get; set; }
        public FlujoSolicitud FlujoSolicitud { get; set; }

        [ForeignKey("PasoFlujoSolicitud")]
        public int PasoFlujoSolicitudId { get; set; }
        public PasoFlujoSolicitud PasoFlujoSolicitud { get; set; }

        [ForeignKey("EstadoLegalizacion")]
        public int? EstadoId { get; set; }
        public EstadoLegalizacion EstadoLegalizacion { get; set; }

        public decimal MontoAnticipoEntregado { get; set; }

        public decimal MontoGastosReportados { get; set; }

        public decimal MontoSaldo { get; set; }

        [Required(ErrorMessage = "Debe indicar Fecha Desde.")]
        [DisplayName("Fecha Desde")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaDesde { get; set; }

        [Required(ErrorMessage = "Debe indicar Fecha Hasta.")]
        [DisplayName("Fecha Hasta")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime FechaHasta { get; set; }

        [Required(ErrorMessage = "Destino es requerido.")]
        [ForeignKey("Destino")]
        public int? DestinoID { get; set; }
        public Destino Destino { get; set; }

        [NotMapped] public Banco Banco { get; set; }

        [NotMapped] public Solicitud Solicitud { get; set; }

        [NotMapped] public KactusEmpleado Empleado { get; set; }

        [NotMapped] public List<SolicitudGastos> SolicitudGastos { get; set; }

        [NotMapped] public List<LegalizacionGastos> LegalizacionGastos { get; set; }

        [NotMapped] public List<Flujo> ListaFlujo { get; set; }

    }
}
