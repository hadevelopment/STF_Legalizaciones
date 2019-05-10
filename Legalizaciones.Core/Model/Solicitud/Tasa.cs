using Legalizaciones.Model.Base;
using Legalizaciones.Model.Jerarquia;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model.ItemSolicitud
{
    [Table("Tasa")]
    public class Tasa: BaseModel
    {
        [ForeignKey("Moneda")]
        public int MonedaId { get; set; }
        public Moneda Moneda { get; set; }

        [Required]
        public string Valor { get; set; }
    }
}
