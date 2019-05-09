using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Jerarquia
{
    public class Ciudad : BaseModel
    {
        [Required]
        public string Nombre { get; set; }

        [ForeignKey("Pais")]
        public int PaisID { get; set; }
        public Pais Pais { get; set; }

    }
}
