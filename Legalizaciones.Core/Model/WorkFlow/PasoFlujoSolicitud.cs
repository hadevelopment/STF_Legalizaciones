using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    public class PasoFlujoSolicitud : BaseModel
    {
        public string Descripcion { get; set; }

        public string CedulaAprobador { get; set; }

        public string NombreAprobador { get; set; }
    }
}
