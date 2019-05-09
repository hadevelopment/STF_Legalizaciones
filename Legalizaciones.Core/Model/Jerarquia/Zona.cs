using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Jerarquia
{
    public class Zona : BaseModel
    {
        [Required]
        public string Nombre { get; set; }

        [ForeignKey("Destino")]
        public int DestinoID { get; set; }
        public Destino Destino { get; set; }

        public string Abreviatura { get; set; }

    }
}
