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
    [Table("KactusEmpleado")]
    public class KactusEmpleado : BaseModel
    {
        [Required]
        public string Cargo { get; set; }
        public string CargoEmpleado { get; set; }
        public string Celular { get; set; }
        public string CentroCosto { get; set; }
        public string CodCiudadExpedicion { get; set; }
        public string CodCiudadResidencia { get; set; }
        public string CodDeptoExpedicion { get; set; }
        public string CodDeptoResidencia { get; set; }
        public int CodTipoEspPersonaPreliq { get; set; }
        public int CodTipoPersona { get; set; }
        public int CodUbicacion { get; set; }
        public string CodigoArea { get; set; }
        public string CodigoEmpresa { get; set; }
        public string CodigoGrupo { get; set; }
        public string CodigoNivel { get; set; }
        public int CodigoNivel1 { get; set; }
        public int CodigoNivel2 { get; set; }
        public int CodigoNivel3 { get; set; }
        public int CodigoNivel4 { get; set; }
        public int CodigoNivel5 { get; set; }
        public int CodigoNivel6 { get; set; }
        public int CodigoNivel7 { get; set; }
        public string Direccion { get; set; }
        public string Email { get; set; }
        public string Eps { get; set; }
        public string EscalaSalarial { get; set; }
        public string EstadoEmpleado { get; set; }
        public string ExtensionCompania { get; set; }
        public DateTime FecActCargo { get; set; }
        public DateTime FecActContr { get; set; }
        public DateTime FechaInicioContrato { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string GanaExtras { get; set; }
        public string Genero { get; set; }
        public string IdentificacionExterna { get; set; }
        public string NombreNivel { get; set; }
        public string NombreNivel1 { get; set; }
        public string NombreNivel2 { get; set; }
        public string NombreNivel3 { get; set; }
        public string NombreNivel4 { get; set; }
        public string NombreNivel5 { get; set; }
        public string NombreNivel6 { get; set; }
        public string NombreNivel7 { get; set; }
        public string NumeroContrato { get; set; }
        public string NumeroDeIdentificacion { get; set; }
        public string PrimerApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string PuedeSerVisitado { get; set; }
        public string Rh { get; set; }
        public string SegundoApellido { get; set; }
        public string SegundoNombre { get; set; }
        public string Telefono { get; set; }
        public string Temporal { get; set; }
        public string TipoContratista { get; set; }
        public string TipoDeIdentificacion { get; set; }
        public string TipoDeSangre { get; set; }
        public bool Titular { get; set; }
        public string ValidoParaLiqNomina { get; set; }
        public int VencimientoAccion { get; set; }
        
    }
}
