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
    public class MatrizDetalle : BaseModel
    {     
        
        public string Nombre { get; set; }
        public string IDMatrizAprobacion { get; set; }
        public string IDAprobador { get; set; }
        public string IDSuplente { get; set; }
        public int Orden { get; set; }      
       
        public virtual MatrizAprobacion MatrizAprobacion { get; set; }
    }
}
