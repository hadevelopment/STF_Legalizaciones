using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Empresa
{
    public class Cargo : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
    }
}
