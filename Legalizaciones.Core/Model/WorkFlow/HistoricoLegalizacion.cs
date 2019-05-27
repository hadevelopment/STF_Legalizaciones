using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Workflow
{
    public class HistoricoLegalizacion : BaseModel
    {
        [ForeignKey("Legalizacion")]
        public int LegalizacionId { get; set; }
        public Legalizacion Legalizacion { get; set; }

        [ForeignKey("FlujoSolicitud")]
        public int FlujoSolicitudId { get; set; }
        public FlujoSolicitud FlujoSolicitud { get; set; }

        [ForeignKey("PasoFlujoSolicitud")]
        public int? PasoFlujoSolicitudId { get; set; }
        public PasoFlujoSolicitud PasoFlujoSolicitud{ get; set; }

        public string Descripcion { get; set; }
    }
}
