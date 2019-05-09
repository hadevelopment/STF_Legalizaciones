using Legalizaciones.Model.Base;
using Legalizaciones.Model.Jerarquia;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model
{
    [Table("Banco")]
    public class Banco: BaseModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}
