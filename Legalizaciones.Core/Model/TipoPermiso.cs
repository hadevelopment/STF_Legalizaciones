using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("TipoPermiso")]
    public class TipoPermiso :  BaseModel 
    {
        [Required]
        public string Descripcion { get; set; }
    }
}
