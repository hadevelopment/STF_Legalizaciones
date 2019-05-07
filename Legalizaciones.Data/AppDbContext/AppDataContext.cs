using Legalizaciones.Model;
using Legalizaciones.Model.Empresa;
using Legalizaciones.Model.Jerarquia;
using Legalizaciones.Model.ItemSolicitud;
using Legalizaciones.Model.WorkFlow;
using Microsoft.EntityFrameworkCore;


namespace Legalizaciones.Data.AppDbContext
{

   
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions options) : base(options)
        {
        }


        //Localidades
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Ciudad> Ciudad { get; set; }
        public DbSet<Destino> Destino { get; set; }
        public DbSet<Zona> Zona { get; set; }
        public DbSet<Ruta> Ruta { get; set; }

        //Empresa
        public DbSet<Compania> Compania { get; set; }
        public DbSet<Gerencia> Gerencia { get; set; }
        public DbSet<Supervisor> Supervisor { get; set; }

        //Solicitud
        public DbSet<Concepto> Concepto { get; set; }
        public DbSet<Moneda> Moneda { get; set; }
        //Objetos
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Producto> Producto { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Inventario> Inventario { get; set; }
        public DbSet<InventarioGastos> InventarioGastos { get; set; }
        public DbSet<Empleado> Empleado { get; set; }
        public DbSet<Solicitud> Solicitud { get; set; }
        public DbSet<SolicitudGastos> SolicitudGastos { get; set; }
        public DbSet<ServicioDetalle> ServicioDetalle { get; set; }
        //WorkFlow
        public DbSet<SolicitudActiva> SolicitudActiva { get; set; }
        public DbSet<FlujoAprobacion> FlujoAprobacion { get; set; }
        public DbSet<MatrizAprobacion> MatrizAprobacion { get; set; }
        public DbSet<MatrizDetalle> MatrizDetalle { get; set; }
        public DbSet<TipoDocumento> TipoDocumento { get; set; }
        public DbSet<TipoSolicitud> TipoSolicitud { get; set; }
        public DbSet<EstadoSolicitud> EstadoSolicitud { get; set; }
        public object Setting { get; private set; }
    }
}
