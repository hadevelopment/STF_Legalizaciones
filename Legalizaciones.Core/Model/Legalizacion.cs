using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("Legalizacion")]
    public class Legalizacion : BaseModel
    {
        public Legalizacion()
        {
        }

        [ForeignKey("Solicitud")]
        public int SolicitudID { get; set; }

        [Required(ErrorMessage = "Recibo de caja es requerido.")]
        public long ReciboCaja { get; set; }

        [Required(ErrorMessage = "Consignacion es requerido.")]
        public long Consignacion { get; set; }

        [Required(ErrorMessage = "Recibo de caja es requerido.")]
        public string Valor { get; set; }

        [ForeignKey("Banco")]
        public int BancoId{ get; set; }

    }
}
