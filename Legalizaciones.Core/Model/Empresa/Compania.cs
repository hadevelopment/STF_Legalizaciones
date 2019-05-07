using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Empresa
{
    public class Compania : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string RazonSocial { get; set; }
        [ForeignKey("Pais")]
        public int PaisId { get; set; }
        public Jerarquia.Pais Pais { get; set; }

        public ICollection<UnidadNegocio> UnidadNegocios { get; set; }
    }
}
