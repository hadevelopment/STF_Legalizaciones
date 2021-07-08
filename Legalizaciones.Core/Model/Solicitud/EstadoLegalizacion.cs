using Legalizaciones.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model.ItemSolicitud
{
    [Table("EstadoLegalizacion")]
    public class EstadoLegalizacion : BaseModel
    {
        [Required]
        public string Descripcion { get; set; }
    }
}
