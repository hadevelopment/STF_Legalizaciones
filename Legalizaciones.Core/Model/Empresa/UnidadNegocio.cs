using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model.Empresa
{
    public class UnidadNegocio : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [ForeignKey("Compania")]
        public int? CompaniaId { get; set; }
        public Empresa.Compania Compania { get; set; }
        public ICollection<Gerencia> Gerencias { get; set; }
    }
}
