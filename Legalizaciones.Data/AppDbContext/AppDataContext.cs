<<<<<<< HEAD
using Legalizaciones.Model;using Legalizaciones.Model.Empresa;using Legalizaciones.Model.Jerarquia;using Legalizaciones.Model.ItemSolicitud;using Microsoft.EntityFrameworkCore;namespace Legalizaciones.Data.AppDbContext{


    public class AppDataContext : DbContext    {        public AppDataContext(DbContextOptions options) : base(options)        {        }
=======

﻿using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;using Legalizaciones.Model.Jerarquia;using Legalizaciones.Model.ItemSolicitud;using Microsoft.EntityFrameworkCore;namespace Legalizaciones.Data.AppDbContext{


    public class AppDataContext : DbContext    {
     
        public AppDataContext(DbContextOptions options) : base(options)        {        }
>>>>>>> 234a901aee6f62d7e29f43386881ecbae3affa5e

        //Configuracion
        public DbSet<ConfiguracionGasto> ConfiguracionGasto { get; set; }

        //Localidades
        public DbSet<Pais> Pais { get; set; }        public DbSet<Ciudad> Ciudad { get; set; }        public DbSet<Destino> Destino { get; set; }        public DbSet<Zona> Zona { get; set; }
<<<<<<< HEAD
=======
        public DbSet<OrigenDestino> OrigenDestino { get; set; }
>>>>>>> 234a901aee6f62d7e29f43386881ecbae3affa5e

        //Solicitud
        public DbSet<Moneda> Moneda { get; set; }
        public DbSet<Tasa> Tasa{ get; set; }
        //Objetos
        public DbSet<Banco> Banco { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<EmpleadoPermiso> EmpleadoPermiso { get; set; }
        public DbSet<Solicitud> Solicitud { get; set; }
        public DbSet<SolicitudGastos> SolicitudGastos { get; set; }
        public DbSet<Legalizacion> Legalizacion { get; set; }
        public DbSet<LegalizacionGastos> LegalizacionGastos { get; set; }
        public DbSet<ServicioDetalle> ServicioDetalle { get; set; }
        //WorkFlow
        public DbSet<FlujoSolicitud> FlujoSolicitud{ get; set; }
        public DbSet<PasoFlujoSolicitud> PasoFlujoSolicitud { get; set; }
        public DbSet<HistoricoSolicitud> HistoricoSolicitud { get; set; }
        public DbSet<TipoSolicitud> TipoSolicitud { get; set; }
        public DbSet<TipoPermiso> TipoPermiso{ get; set; }
        public DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
<<<<<<< HEAD
        public DbSet<OrigenDestino> OrigenDestino { get; set; }
        public object Setting { get; private set; }
    }
}
=======
        public object Setting { get; private set; }
    }
}

>>>>>>> 234a901aee6f62d7e29f43386881ecbae3affa5e
