﻿using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Workflow
{
    public class Flujo
    {
        public int Id{ get; set; }

        public string Descripcion { get; set; }

        public string NivelAprobador { get; set; }

        public string DescripcionAprobador { get; set; }

        public int Orden{ get; set; }

        public string TipoSolicitud { get; set; }

        public string Motivo { get; set; }
        public int Procesado { get; set; }
    }
}
