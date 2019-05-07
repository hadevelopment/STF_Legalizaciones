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
    [Table("Empleado")]
    public class Empleado : BaseModel
    {
        [Required]
        public string Cedula { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int? CargoId { get; set; }
        public string Area { get; set; }
        public string Telefono { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string NombreSupervisor { get; set; }
        public int SupervisorId { get; set; }
    }
}
