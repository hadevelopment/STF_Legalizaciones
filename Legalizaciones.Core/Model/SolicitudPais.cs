using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("SolicitudPais")]
    public class SolicitudPais : BaseModel
    {
        [ForeignKey("Solicitud")]
        public int? SolicitudID { get; set; }
        public Solicitud Solicitud { get; set; }

        [ForeignKey("Pais")]
        public int? PaisID { get; set; }
        public Pais Pais { get; set; }

        [ForeignKey("Estado")]
        public int? EstadoID { get; set; }
        public Ciudad Estado { get; set; }
        
    }
 }
