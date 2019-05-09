using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.ItemSolicitud
{
    public class Moneda : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        public byte Simbolo { get; set; }
        public string Abreviatura { get; set; }
    }
}
