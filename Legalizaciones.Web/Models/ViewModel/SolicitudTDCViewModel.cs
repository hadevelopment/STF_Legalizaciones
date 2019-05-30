using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Legalizaciones.Web.Models.ViewModel
{
    public class SolicitudTDCViewModel
    {
        [Required(ErrorMessage = "Debe indicar una Cedula.")]
        public string Cedula {get;set;}

        [Required(ErrorMessage = "Debe indicar una Tarjeta de Credito.")]
        public string Extracto { get; set; }

        [Required(ErrorMessage = "Debe indicar un nombre de Banco.")]
        public string Banco { get; set; }

        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Debe indicar un Monto valido.")]
        [Required(ErrorMessage = "Debe indicar un Monto valido.")]
        [DisplayName("Monto")]
        public decimal Monto { get; set; }

        public int id { get; set; }
        
    }
}
