using Legalizaciones.Model.Base;
using Legalizaciones.Model.WorkFlow;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    public class FlujoAprobacion : BaseModel
    {        
        public string Nombre { get; set; }
        public string IDDocumento { get; set; }
        public string IDDestino { get; set; }
        public float MontoDesde { get; set; }
        public float MontoHasta { get; set; }
        public string IDMoneda { get; set; }
        public string IDMAprobacion { get; set; }              
        public int TipoDocumentoId { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
    }
}
