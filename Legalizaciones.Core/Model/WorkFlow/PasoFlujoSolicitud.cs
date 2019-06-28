using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Workflow
{
    public class PasoFlujoSolicitud : BaseModel
    {
        [ForeignKey("FlujoSolicitud")]
        public int FlujoSolicitudId { get; set; }
        public FlujoSolicitud FlujoSolicitud { get; set; }

        public string Descripcion { get; set; }

        public int Orden { get; set; }

        public string NivelAprobador { get; set; }

        public string DescripcionAprobador { get; set; }

        public string NivelSuplenteUno { get; set; }

        public string DescripcionSuplenteUno { get; set; }

        public string NivelSuplenteDos { get; set; }

        public string DescripcionSuplenteDos { get; set; }

    }
}
