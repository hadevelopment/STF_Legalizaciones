﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Legalizaciones.Web.Models
{
    public class AprobacionDocumento
    {
        public string TipoSeleccionado { get; set; }
        public int CountDocAsociado { get; set; }
        public List<string> TipoSolicitud { get; set; } 
        public List <DataAprobacion> Aprobadores { get; set; }
        public List<DataAprobacion> FlujoAprobacion { get; set; }
    }

    public class DataAprobacion
    {
        public string NombreAprobador { get; set; }
        public string CedulaAprobador { get; set; }
        public string EmailAprobador { get; set; }
        public int  Orden { get; set; }
        public int FlujoSolicitudId { get; set; }
        public int Estatus { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaCreacion { get { return DateTime.Now; } }
        public int Update { get; set; }
        public string TipoSolicitud { get; set; }
        public int IdTipoSolicitud { get; set; }
        public int Id { get; set; }
        public int DestinoId { get; set; }
        public string Destino { get; set; }
        public float MontoMinimo { get; set; }
        public float MontoMaximo { get; set; }
        public string AprobadorSuplente1 { get; set; }
        public string CedulaSuplente1 { get; set; }
        public string EmailSuplente1 { get; set; }
        public string AprobadorSuplente2 { get; set; }
        public string CedulaSuplente2 { get; set; }
        public string EmailSuplente2 { get; set; }
    }

    public class Destinos
    {
        public int Id { get; set; }
        public string Destino { get; set; }
    }

    public class Monedas
    {
        public int Id { get; set; }
        public string Moneda { get; set; }
    }

    public class DocumentoTipo
    {
        public int Id { get; set; }
        public string Documento { get; set; }
    }

    public class FlujoDescripcion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
    }
}
