using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    public class HistoricoSolicitud : BaseModel
    {
        [ForeignKey("Solicitud")]
        public int SolicitudId { get; set; }

        [ForeignKey("FlujoSolicitud")]
        public int FlujoSolicitudId { get; set; }

        [ForeignKey("PasoFlujoSolicitud")]
        public int PasoFlujoSolicitudId { get; set; }

        public string Descripcion { get; set; }
    }
}
