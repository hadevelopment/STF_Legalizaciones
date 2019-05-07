using Legalizaciones.Model.Base;
using Legalizaciones.Model.Jerarquia;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model
{
    [Table("ServicioDetalle")]
    public class ServicioDetalle : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [ForeignKey("Pais"), Column(Order = 0)]
        [DisplayName("Pais")]
        public int PaisID { get; set; }
        public Pais Pais { get; set; }

        public string Cargo { get; set; }

        [Required]
        public float Monto { get; set; }
        public string Descripcion { get; set; }

        public string TipoServicio { get; set; }

        [ForeignKey("Zona"), Column(Order = 0)]
        [DisplayName("Origen")]
        public int ZonaOrigenId { get; set; }
        public Zona Origen { get; set; }

        [ForeignKey("Zona"), Column(Order = 1)]
        [DisplayName("Destino")]
        public int ZonaDestinoId { get; set; }
        public Zona Destino { get; set; }

        public int ProveedorID { get; set; }
        public string Proveedor { get; set; }
    }
}
