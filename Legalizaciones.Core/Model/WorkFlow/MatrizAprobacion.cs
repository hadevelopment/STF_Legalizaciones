using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.WorkFlow
{
    public class MatrizAprobacion : BaseModel
    {
        public MatrizAprobacion()
        {
            MatrizDetalles = new List<MatrizDetalle>();
        }

        public string Nombre { get; set; }
        public int FlujoAprobacionID { get; set; }
        public virtual FlujoAprobacion FlujoAprobacion { get; set; }
        public virtual ICollection<MatrizDetalle> MatrizDetalles { get; set; }
    }
}
