using Legalizaciones.Model.Base;
using Legalizaciones.Model.ItemSolicitud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Jerarquia
{
    public class Destino : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        public ICollection<Zona> Ciudad { get; set; }

    }
}
