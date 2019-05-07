using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("Inventario")]
    public class Inventario : BaseModel
    {

        public Inventario()
        {
            InventarioGastos = new List<InventarioGastos>();
        }

        [Required]
        public float Total { get; set; }
        public string Activo { get; set; }
        public float Anticipo { get; set; }
        public float ChangeAmount { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteID { get; set; }
        public Cliente Cliente { get; set; }

        public ICollection<InventarioGastos> InventarioGastos { get; set; }
    }
}
