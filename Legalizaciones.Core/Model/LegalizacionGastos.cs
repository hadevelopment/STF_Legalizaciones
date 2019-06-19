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

        public string FechaGasto { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Operaciones.")]
        public int CentroOperacionId { get; set; }
        public string CentroOperacion { get; set; }

        [NotMapped]
        public CentroOperacion CentroOperacionObj { get; set; }

        [Required(ErrorMessage = "Seleccione una Unidad de Negocio.")]
        public int UnidadNegocioId { get; set; }
        public string UnidadNegocio { get; set; }

        [NotMapped]
        public UnidadNegocio UnidadNegocioObj{ get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Costo.")]
        public int CentroCostoId { get; set; }
        public string CentroCosto { get; set; }

        [NotMapped]
        public CentroCosto CentroCostoObj { get; set; }

        [Required(ErrorMessage = "Seleccione un Motivo.")]
        public int MotivoId { get; set; }

        [ForeignKey("Pais")]
        public int PaisId { get; set; }
        public string Pais { get; set; }

        [ForeignKey("Ciudad")]
        public int CiudadId { get; set; }
        public string Ciudad { get; set; }

        [Required(ErrorMessage = "Seleccione un Tipo de Servicio.")]
        public int TipoServicioId { get; set; }
        public string Servicio { get; set; }

        [Required(ErrorMessage = "Seleccione un Proveedor.")]
        public int ProveedorId { get; set; }

        [NotMapped]
        public Pais PaisObj { get; set; }

        [NotMapped]
        public Ciudad CiudadObj { get; set; }

        //Para que me carge los Motivo
        [NotMapped]
        public Motivo Motivo { get; set; }

        public string Concepto{ get; set; }

        public string  Valor { get; set; }
        public string IVA { get; set; }
        public string ReteIVA { get; set; }
        public string ReteServicio { get; set; }
        public string ICA { get; set; }
        public string Neto { get; set; }
        public string IVATeorico { get; set; }
    }
}
