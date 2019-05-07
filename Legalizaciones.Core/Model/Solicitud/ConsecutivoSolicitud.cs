using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.ItemSolicitud
{
    public class ConsecutivoSolicitud : BaseModel
    {
        public int Actual { get; set; }
    }
}
