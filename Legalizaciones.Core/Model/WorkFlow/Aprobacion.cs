using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Workflow
{
    public class Aprobacion
    {
        public Aprobacion()
        {
            PasosAsignadosAnticipo = new List<PasoFlujoSolicitud>();
            PasosAsignadosLegalizacion = new List<PasoFlujoSolicitud>();
            Anticipos = new List<Solicitud>();
            Legalizaciones = new List<Legalizacion>();
        }

        public List<PasoFlujoSolicitud> PasosAsignadosAnticipo { get; set; }
        public List<PasoFlujoSolicitud> PasosAsignadosLegalizacion { get; set; }
        public List<Solicitud> Anticipos { get; set; }
        public List<Legalizacion> Legalizaciones { get; set; }
    }
}
