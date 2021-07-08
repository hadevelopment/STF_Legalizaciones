using Legalizaciones.Model.Base;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.Jerarquia;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Legalizaciones.Model
{
    [Table("ConfiguracionGasto")]
    public class ConfiguracionGasto : BaseModel
    {
        [Required(ErrorMessage = "Descripción es requerida.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Cargo es requerido.")]
        public string CargoId { get; set; }
        public string Cargo { get; set; }

        [Required(ErrorMessage = "País es requerido.")]
        [DisplayName("Pais")]
        [ForeignKey("Pais"), Column(Order = 0)]
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
        public string PaisNombre { get; set; }

        [Required(ErrorMessage = "Tipo de Servicio es requerido.")]
        public string TipoServicioId { get; set; }
        public string TipoServicio { get; set; }

        [ForeignKey("Zona"), Column(Order = 0)]
        [DisplayName("Origen")]
        public int ZonaOrigenId { get; set; }
        public Zona Origen { get; set; }
        public string OrigenNombre { get; set; }

        [ForeignKey("Zona"), Column(Order = 1)]
        [DisplayName("Destino")]
        public int ZonaDestinoId { get; set; }
        public Zona Destino { get; set; }
        public string DestinoNombre { get; set; }

        [Required(ErrorMessage = "Moneda es requerida.")]
        [ForeignKey("Moneda")]
        public int MonedaId { get; set; }
        public Moneda Moneda { get; set; }
        public string MonedaNombre { get; set; }

        public bool GastoDiario { get; set; }

        [Required(ErrorMessage = "Monto es requerido.")]
        public float Monto { get; set; }
    }
}
