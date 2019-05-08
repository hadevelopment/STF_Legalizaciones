using Legalizaciones.Model.Base;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("ServicioDetalle")]
    public class ServicioDetalle : BaseModel
    {
        [Required]
        public string Nombre { get; set; }
        [ForeignKey("Pais"), Column(Order = 0)]
        [DisplayName("Pais")]
        public int PaisID { get; set; }
        public Pais Pais { get; set; }

        [ForeignKey("Cargo")]
        [DisplayName("Cargo")]
        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        [Required]
        public float Monto { get; set; }
        public string Descripcion { get; set; }

        [DisplayName("Tipo Servicio")]
        [Required]
        public int TipoServicioID { get; set; }
        public TipoServicio TipoServicio { get; set; }

        [ForeignKey("Zona"), Column(Order = 0)]
        [DisplayName("Origen")]
        public int ZonaOrigenId { get; set; }
        public Zona Origen { get; set; }

        [ForeignKey("Zona"), Column(Order = 1)]
        [DisplayName("Destino")]
        public int ZonaDestinoId { get; set; }
        public Zona Destino { get; set; }

        [DisplayName("Proveedor")]
        [Required]
        public int ProveedorID { get; set; }
        public Proveedor Proveedor { get; set; }
    }
}
