﻿using System;
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
using Microsoft.AspNetCore.Builder;



using DinkToPdf;
using DinkToPdf.Contracts;
using Legalizaciones.Web.Helpers;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Legalizaciones
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddDbContext<AppDataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddTransient<ISolicitudRepository, SolicitudRepository>();
            services.AddTransient<ISolicitudGastosRepository, SolicitudGastosRepository>();
            services.AddTransient<ITipoSolicitudRepository, TipoSolicitudRepository>();

            //IJerarquia
            services.AddTransient<ICiudadRepository, CiudadRepository>();
            services.AddTransient<IPaisRepository, PaisRepository>();
            //ISolicitud
            services.AddTransient<IConceptoRepository, ConceptoRepository>();
            services.AddTransient<IDestinoRepository, DestinoRepository>();
            services.AddTransient<IMonedaRepository, MonedaRepository>();
            services.AddTransient<IZonaRepository, ZonaRepository>();
            services.AddTransient<IEstadoSolicitudRepository, EstadoSolicitudRepository>();

            var context = new CustomAssemblyLoadContext();
            context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libwkhtmltox.dll"));

            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddMvc();
            services.AddDistributedMemoryCache(); // Adds a default in-memory implementation of IDistributedCache
            services.AddSession();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
