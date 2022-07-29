using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using mmc.AccesoDatos.Data;
using mmc.AccesoDatos.Repositorios;
using mmc.AccesoDatos.Repositorios.IRepositorio;
using mmc.Utilidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//para desabilitar el https en una aplicacion  **************** #1
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;

namespace mmc
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();


            //codigo Original
            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)

            services.AddIdentity<IdentityUser, IdentityRole>().AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            //este servisio lo agregamos para la confirmacion de email con los datos de la clase que creamos en nuestro proyecto Utilidades
            services.AddSingleton<IEmailSender, EmailSender>();

            //por medio de inyeccion de dependencias inyectamos Nuestra Unida de trabajo y la interface de IUnidadtrabajo *********************************************************
            services.AddScoped<IUnidadTrabajo, UnidadTrabajo>();
            /////////////////////////////////////////////////////////////////////////////////////////////////////////////
            ///
            services.AddControllersWithViews();
            //agregamos este servisio para que soporte vistas de razor


            //para desabilitar el https en una aplicacion ***************************************************************CODIGOPARA QUITARHTTPS #2
            services.Configure<ForwardedHeadersOptions>(options =>
            {
                options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
                //OPCIONAL CUANDO EL PROXY ESTA EN OTRO EQUIPO PERO SI ESTA EN EL MISNO NO ES NECESARIO
                options.KnownProxies.Add(IPAddress.Parse("192.168.1.156"));

            });

            services.AddRazorPages();

            ///este codigo fue sacado de la pagina oficial de Microsoft para que haga las correctas validaciones Identity 
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath =$"/Identity/Account/AccessDenied";
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //if (!app.Environment.IsDevelopment())
            //{
            //    app.UseHttpsRedirection();
            //}
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                //para desabilitar el https en una aplicacion ***************************************************************CODIGOPARA QUITARHTTPS #3
                app.UseHttpsRedirection();
                app.UseForwardedHeaders();
            }
            //if (env.IsProduction() || env.IsStaging() || env.IsEnvironment("Staging_2"))
            //{
            //    app.UseExceptionHandler("/Error");
            //}
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //para desabilitar el https en una aplicacion ************************************************************************CODIGOPARA QUITARHTTPS #4
                app.UseForwardedHeaders();
                app.UseHttpsRedirection();
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();//aqui abilitamos la autenticacion en el proyecto
            app.UseAuthorization();//aqui Habilitamos la autorizacion en el proyecto

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{area=HelpDesk}/{controller=Home}/{action=Index}/{id?}");
                    //pattern: "{area=HelpDesk}/{controller=Home}/{action=Index}/{estadoId?}");
                endpoints.MapRazorPages();
            });
        }
    }
}

