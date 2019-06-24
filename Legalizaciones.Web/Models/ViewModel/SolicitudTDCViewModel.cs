using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Legalizaciones.Web.Models.ViewModel
{
    public class SolicitudTDCViewModel
    {
        [Required(ErrorMessage = "Seleccione un Centro de Operaciones.")]
        public int? CentroOperacionId { get; set; }
        public string CentroOperacion { get; set; }
        public CentroOperacion CentroOperacionObj { get; set; }

        [Required(ErrorMessage = "Seleccione una Unidad de Negocio.")]
        [DisplayName("Unidad Negocio")]
        public int? UnidadNegocioId { get; set; }
        public string UnidadNegocio { get; set; }
        public UnidadNegocio UnidadNegocioObj { get; set; }

        [Required(ErrorMessage = "Seleccione un Centro de Costo.")]
        [DisplayName("Centro Costo")]
        public int? CentroCostoId { get; set; }
        public string CentroCosto { get; set; }
        public CentroCosto CentroCostoObj { get; set; }

        [Required(ErrorMessage = "Debe indicar una Cedula.")]
        //[RegularExpression(@"^[a-zA-Z0-9]{4,10}$")]
        public string Cedula {get;set;}

        [Required(ErrorMessage = "Debe indicar una Tarjeta de Credito.")]
        public string Extracto { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Banco.")]
        //[RegularExpression(@"^[a-zA-Z0-9]{4,10}$", ErrorMessage = "Debe indicar un nombre del Banco valido.")]
        public string BancoId { get; set; }
        public SelectList ListaBanco { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un Banco.")]
        //[RegularExpression(@"^[a-zA-Z0-9]{4,10}$", ErrorMessage = "Debe indicar un nombre del Banco valido.")]
        public string DestinoId { get; set; }
        public SelectList ListaDestino { get; set; }

        [Range(1, (double)decimal.MaxValue, ErrorMessage = "Debe indicar un Monto valido.")]
        [Required(ErrorMessage = "Debe indicar un Monto valido.")]
        [DisplayName("Monto")]
        public decimal Monto { get; set; }

        public int id { get; set; }

        public IFormFile Archivo { get; set; }
    }
}
