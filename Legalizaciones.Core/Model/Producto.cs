using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("Producto")]
    public class Producto : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public float Precio { get; set; }
        public string Descripcion { get; set; }
        [ForeignKey("Categoria")]
        [Required]
        public int CategoriaID { get; set; }
        public Categoria Categoria { get; set; }


    }
}
