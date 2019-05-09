using Legalizaciones.Model.Base;
using Legalizaciones.Model.Jerarquia;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model
{
    public class TipoServicio: BaseModel
    {
        [Required]
        public string Nombre { get; set; }
    }
}
