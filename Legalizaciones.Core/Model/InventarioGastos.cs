using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("InventarioGastos")]
    public class InventarioGastos : BaseModel
    {
        [Required]
        public int Cantidad { get; set; }
        [Required]
        [DisplayName("Total")]
        public float Total { get; set; }
        [DisplayName("Precio")]
        public float Precio { get; set; }


        [ForeignKey("Producto")]
        public int ProductoID { get; set; }
        public Producto Producto { get; set; }

        [ForeignKey("Inventario")]
        public int InventarioID { get; set; }
        public Inventario Inventario { get; set; }
    }
}
