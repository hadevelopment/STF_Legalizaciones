using System;

namespace Legalizaciones.Web.Models
{
    public class SolicitudAnticipioView
    {
        public int    Id              { get; set; }
        public string NumeroSolicitud { get; set; }
        public string FechaSolicitud  { get; set; }
        public string Concepto        { get; set; }
        public string CedulaBenefi    { get; set; }
        public string Beneficiario    { get; set; }
        public string Monto           { get; set; }
        public string EstadoSoli      { get; set; }
        public string FechaPago       { get; set; }
    }
}
