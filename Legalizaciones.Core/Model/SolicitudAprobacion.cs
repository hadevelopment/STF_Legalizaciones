using Legalizaciones.Model.Base;
using Legalizaciones.Model.Workflow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("SolicitudAprobacion")]
    public class SolicitudAprobacion : BaseModel
    {
        [ForeignKey("Solicitud")]
        public int SolicitudId { get; set; }
        public Solicitud Solicitud { get; set; }

        [ForeignKey("PasoFlujoSolicitud")]
        public int? PasoFlujoSolicitudId { get; set; }
        public PasoFlujoSolicitud PasoFlujoSolicitud { get; set; }

        public int Orden { get; set; }
        public string Aprobador { get; set; }
        public string SuplenteUno { get; set; }
        public string SuplenteDos { get; set; }
    }
}
