﻿using Legalizaciones.Model.Base;
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

        public string CedulaAprobador { get; set; }

        public string NombreAprobador { get; set; }

        public string EmailAprobador { get; set; }

    }
}
