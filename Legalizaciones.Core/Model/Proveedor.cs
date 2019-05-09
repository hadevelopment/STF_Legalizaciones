using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    public class Proveedor : BaseModel
    {
        [Required]
        public string Nombre { get; set; }

        public virtual ICollection<ServicioDetalle> Servicios { get; set; }

        public Proveedor()
        {
            Servicios = new List<ServicioDetalle>();
        }


    }
}
