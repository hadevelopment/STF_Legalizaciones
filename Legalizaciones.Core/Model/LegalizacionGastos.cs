using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Legalizaciones.Model.Jerarquia;

namespace Legalizaciones.Model
{
    [Table("LegalizacionGastos")]
    public class LegalizacionGastos : BaseModel
    {
        [ForeignKey("Legalizacion")]
        public int? LegalizacionId { get; set; }
        public Legalizacion Legalizacion { get; set; }

        public DateTime FechaGasto { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Operaciones.")]
        public int CentroOperacionId { get; set; }

        [NotMapped]
        public CentroOperacion CentroOperacion { get; set; }

        [Required(ErrorMessage = "Seleccione una Unidad de Negocio.")]
        public int UnidadNegocioId { get; set; }

        [NotMapped]
        public UnidadNegocio UnidadNegocio{ get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Costo.")]
        public int CentroCostosId { get; set; }

        [NotMapped]
        public CentroCosto CentroCosto { get; set; }

        [Required(ErrorMessage = "Seleccione un Motivo.")]
        public int MotivoId { get; set; }

        [ForeignKey("Pais")]
        public int PaisId { get; set; }

        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }

        [Required(ErrorMessage = "Seleccione un Tipo de Servicio.")]
        public int TipoServicioId { get; set; }

        [Required(ErrorMessage = "Seleccione un Proveedor.")]
        public int ProveedorId { get; set; }

        [NotMapped]
        public Pais Pais { get; set; }

        [NotMapped]
        public Ciudad Ciudad { get; set; }

        //Para que me carge los Motivo
        [NotMapped]
        public Motivo Motivo { get; set; }

        public string Concepto{ get; set; }
        public decimal Valor { get; set; }


    }
}
