using Legalizaciones.Model.Base;
using Legalizaciones.Model.Jerarquia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("Ruta")]
    public class Ruta : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [ForeignKey("Zona")]
        public int Origen_ZonaID { get; set; }
        [NotMapped]
        public Zona Zona_Origen { get; set; }
        [ForeignKey("Zona")]
        public int Destino_ZonaID { get; set; }
        [NotMapped]
        public Zona Zona_Destino { get; set; }
        [ForeignKey("Ciudad")]
        public int CiudadID { get; set; }
        public float Tarifa { get; set; }

    }
}
