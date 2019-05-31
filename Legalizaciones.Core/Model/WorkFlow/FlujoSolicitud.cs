using Legalizaciones.Model.Base;
using Legalizaciones.Model.Jerarquia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Workflow
{
    public class FlujoSolicitud : BaseModel
    {
        [ForeignKey("TipoSolicitud")]
        public int TipoSolicitudId { get; set; }
        public TipoSolicitud TipoSolicitud { get; set; }

        [ForeignKey("Destino")]
        public int DestinoId { get; set; }
        public Destino Destino { get; set; }

        public float MontoMinimo { get; set; }

        public float MontoMaximo { get; set; }
    }
}
