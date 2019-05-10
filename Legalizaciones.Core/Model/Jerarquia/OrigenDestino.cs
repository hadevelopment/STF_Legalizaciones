using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Jerarquia
{
    public class OrigenDestino : BaseModel
    {
        public string Nombre { get; set; }

        public int PaisId { get; set; }
    }
}
