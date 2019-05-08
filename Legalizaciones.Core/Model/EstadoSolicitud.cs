using Legalizaciones.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model
{
    [Table("EstadoSolicitud")]
    public class EstadoSolicitud : BaseModel
    {
        [Required]
        public string Descripcion { get; set; }
    }
}
