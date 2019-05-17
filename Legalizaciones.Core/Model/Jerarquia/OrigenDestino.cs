using System.ComponentModel.DataAnnotations.Schema;
using Legalizaciones.Model.Base;

namespace Legalizaciones.Model.Jerarquia
{
    public class OrigenDestino : BaseModel
    {
        public string Nombre { get; set; }

        public int PaisId { get; set; }

        [NotMapped]
        public Pais Pais { get; set; }
    }
}
