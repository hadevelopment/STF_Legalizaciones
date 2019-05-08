using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("EmpleadoPermiso")]
    public class EmpleadoPermiso : BaseModel
    {
        [Required]
        [ForeignKey("TipoPermiso")]
        public int TipoPermisoId { get; set; }
        public TipoPermiso TipoPermiso { get; set; }

        public string EmpleadoCedula { get; set; }
        public string EmpleadoPermisoCedula { get; set; }
        public string EmpleadoPermisoNombre { get; set; }
    }
}
