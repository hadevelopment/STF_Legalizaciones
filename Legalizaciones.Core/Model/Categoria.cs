using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("Categoria")]
    public class Categoria : BaseModel
    {
        [Required]
        public string Nombre{ get; set; }
        public string Codigo { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }

        public Categoria()
        {
            Productos = new List<Producto>();
        }
        
    }
}
