using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    public class TipoSolicitud :  BaseModel 
    {
        [Required]
        public string Nombre { get; set; }
        public int DiasHabiles { get; set; }
        public string Descripcion { get; set; }

        [ForeignKey("FlujoSolicitud")]
        public int FlujoSolicitudId { get; set; }
    }
}
