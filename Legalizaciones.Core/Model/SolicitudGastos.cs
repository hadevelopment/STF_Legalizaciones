using Legalizaciones.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Legalizaciones.Model
{
    [Table("SolicitudGastos")]
    public class SolicitudGastos : BaseModel
    {
        public DateTime FechaGasto { get; set; }
        public int PaisId{ get; set; }
        public string Pais { get; set; }
        public int? ServicioId { get; set; }
        public string Servicio { get; set; }
        public int CiudadId { get; set; }
        public string Ciudad { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public decimal Monto { get; set; }
        public string IVA { get; set; }
        public string ReteIVA { get; set; }
        public string ReteServicio { get; set; }
        public string ICA { get; set; }
        public string Neto { get; set; }
        public string IVATeorico { get; set; }
        //public string Zona { get; set; }


        [ForeignKey("Solicitud")]
        public int? SolicitudId { get; set; }
        public Solicitud Solicitud { get; set; }
    }
}
