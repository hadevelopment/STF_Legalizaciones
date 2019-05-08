using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.ItemSolicitud
{
    public class Concepto : BaseModel
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public int Activo { get; set; }
    }
}
