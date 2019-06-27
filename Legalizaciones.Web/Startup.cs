using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Legalizaciones.Interface;
using Legalizaciones.Interface.IJerarquia;
using Legalizaciones.Interface.ISolicitud;
using Legalizaciones.Data.AppDbContext;
using Legalizaciones.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DinkToPdf;
using DinkToPdf.Contracts;
using Legalizaciones.Web.Helpers;
using System.IO;
using Legalizaciones.Data.Repository.Jerarquia;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Legalizaciones.Web.Engine;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Quartz.Spi;
using Quartz;
using Quartz.Impl;

namespace Legalizaciones
{
    public class Startup
    {
        private IConfigurationBuilder builder;
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = configuration;
            this.builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddEnvironmentVariables();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            //Configuracion
            services.AddTransient<IConfiguracionGastoRepository, ConfiguracionGastoRepository>();
            //Localidad
            services.AddTransient<ICiudadRepository, CiudadRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            //ISolicitud

            //registering repositories to service 
            services.AddTransient<IBancoRepository, BancoRepository>();
            services.AddTransient<IEmpleadoPermisoRepository, EmpleadoPermisoRepository>();
            services.AddTransient<ISolicitudRepository, SolicitudRepository>();
            services.AddTransient<ISolicitudGastosRepository, SolicitudGastosRepository>();
            services.AddTransient<IHistoricoSolicitudRepository, HistoricoSolicitudRepository>();
            services.AddTransient<ILegalizacionRepository, LegalizacionRepository>();
            services.AddTransient<ILegalizacionGastosRepository, LegalizacionGastosRepository>();
            services.AddTransient<ITipoSolicitudRepository, TipoSolicitudRepository>();
            services.AddTransient<IConceptoRepository, ConceptoRepository>();
            services.AddTransient<IDestinoRepository, DestinoRepository>();
            services.AddTransient<IMonedaRepository, MonedaRepository>();
            services.AddTransient<IZonaRepository, ZonaRepository>();
            services.AddTransient<IOrigenDestinoRepository, OrigenDestinoRepository>();
            services.AddTransient<IEstadoSolicitudRepository, EstadoSolicitudRepository>();
            services.AddTransient<IEmpleadoRepository, EmpleadoRepository>();
            services.AddTransient<ITasaRepository, TasaRepository>();
            services.AddTransient<IFlujoSolicitudRepository, FlujoSolicitudRepository>();
            services.AddTransient<IPasoFlujoSolicitudRepository, PasoFlujoSolicitudRepository>();
            //Iempl
            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IEmail, Email>();

            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

            //services.Configure<ProjectSettings>(Configuration.GetSection("ProjectSettings"));
            EngineDb.DefaultConnection = Configuration["ConnectionStrings:Default"];
            EngineDb.DefaultConnection = Configuration["ConnectionStrings:Default"];
            EngineStf.UserWcf = Configuration["KeyConnect:UserWcf"];
            EngineStf.PasswordWcf = Configuration["KeyConnect:PasswordWcf"];

            // Add Quartz services
            services.AddSingleton<IJobFactory, SingletonJobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            // Add our job
            services.AddSingleton<ChronJob>();
            //services.AddSingleton(new JobSchedule(jobType: typeof(ChronJob),cronExpression: "0 20 04 ? * *"));
            services.AddSingleton(new JobSchedule(jobType: typeof(ChronJob), cronExpression: "0 05 20 ? * *"));
            services.AddHostedService<QuartzHostedService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder service, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                service.UseDeveloperExceptionPage();
                service.UseBrowserLink();
            }
            else
            {
                service.UseExceptionHandler("/Home/Error");
            }

            service.UseStaticFiles();
            service.UseSession();
            //*********************Cultura: English United State -> Formato Numerico ********************************
            var infoCultura = new CultureInfo("en-US");
            service.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(infoCultura),
                SupportedCultures = new List<CultureInfo>
                {
                  infoCultura,
                },
                SupportedUICultures = new List<CultureInfo>
                {
                  infoCultura,
                }
            });
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            this.builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
            //********************************************************************************************************
            service.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
