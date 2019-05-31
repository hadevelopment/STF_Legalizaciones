using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("SolicitudAprobador")]
    public class SolicitudAprobador : BaseModel
    {
        [ForeignKey("Solicitud")]
        public int SolicitudId { get; set; }
        public Solicitud Solicitud { get; set; }

        public string CedulaAprobador { get; set; }
        public string NombreAprobador { get; set; }
        public string EmailAprobador { get; set; }
    }
}
