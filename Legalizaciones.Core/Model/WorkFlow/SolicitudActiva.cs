using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.WorkFlow
{
    public class SolicitudActiva : BaseModel
    {        
        public int SolicitudID { get; set; }
        public string Nombre { get; set; }
        public int IDEmpleado { get; set; }
        public int IDFlujo { get; set; }
        public int CantidadPasos { get; set; }
        public int PosicionActual { get; set; }
        public int Estatus { get; set; }
        public int IDSolicitud { get; set; }
        public string UltimoAprobador { get; set; }
        public string AprobadorSiguiente { get; set; }
        public string MotivoRechazo { get; set; }
        public string Observacion { get; set; }
        public string DocERP { get; set; }
        public string ColorEstatus { get; set; }
        public DateTime FechaERP { get; set; }
       
        public int Estado { get; set; }

        public virtual Solicitud Solicitud { get; set; }
    }
}
