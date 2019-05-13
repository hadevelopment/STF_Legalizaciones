﻿using System.Collections.Generic;
using Legalizaciones.Model.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Legalizaciones.Model
{
    [Table("Legalizacion")]
    public class Legalizacion : BaseModel
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Legalizacion()
        {
            //Solicitud          = new HashSet<Solicitud>();
            //SolicitudGastos    = new HashSet<SolicitudGastos>();
            //LegalizacionGastos = new HashSet<LegalizacionGastos>();
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

        [NotMapped] public Banco Banco { get; set; }

        [NotMapped] public Solicitud Solicitud { get; set; }

        [NotMapped] public Empleado Empleado { get; set; }

        [NotMapped] public List<SolicitudGastos> SolicitudGastos { get; set; }

        [NotMapped] public List<LegalizacionGastos> LegalizacionGastos { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual Solicitud Solicitud { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<SolicitudGastos> SolicitudGastos { get; set; }

        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<LegalizacionGastos> LegalizacionGastos { get; set; }


    }
}
