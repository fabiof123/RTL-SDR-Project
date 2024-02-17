using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Castle.Facilities.Logging;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.Castle.Logging.Log4Net;
using RtlSdrServer.Authentication.JwtBearer;
using RtlSdrServer.Configuration;
using RtlSdrServer.Identity;
using RtlSdrServer.Web.Resources;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Dependency;
using Abp.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.WebEncoders;
using Newtonsoft.Json.Serialization;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Abp.PlugIns;
using System.IO;


namespace RtlSdrServer.Web.Startup
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env)
        {
            _hostingEnvironment = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // MVC
            services.AddControllersWithViews(
                    options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                        options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());
                    }
                )
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new AbpMvcContractResolver(IocManager.Instance)
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    };
                });

            IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            services.Configure<WebEncoderOptions>(options =>
            {
                options.TextEncoderSettings = new TextEncoderSettings(UnicodeRanges.All);
            });
            
            services.AddScoped<IWebResourceManager, WebResourceManager>();

            services.AddSignalR();

            // Configure Abp and Dependency Injection
            /*services.AddAbpWithoutCreatingServiceProvider<RtlSdrServerWebMvcModule>(
                // Configure Log4Net logging
                options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig(
                        _hostingEnvironment.IsDevelopment()
                            ? "log4net.config"
                            : "log4net.Production.config"
                        )
                )
            );*/

            // AGGIUNTO SWAGGER
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "RtlSdr API", Version = "v1" });
                options.DocInclusionPredicate((docName, description) => true);
            });
            // AGGIUNGI PLUGINS
            /*services.AddAbp<RtlSdrServerWebMvcModule>(
                options => options.PlugInSources.AddFolder(Path.Combine(_hostingEnvironment.WebRootPath, "Plugins"), SearchOption.AllDirectories)
                );*/
            //CONFIGURAZIONE Abp and Dependency Injection
            services.AddAbpWithoutCreatingServiceProvider<RtlSdrServerWebMvcModule>(options =>
            {
                //Configure Log4Net logging
                options.IocManager.IocContainer.AddFacility<LoggingFacility>(
                    f => f.UseAbpLog4Net().WithConfig(_hostingEnvironment.IsDevelopment()
                        ? "log4net.config"
                        : "log4net.Production.config")
                );

                options.PlugInSources.AddFolder(Path.Combine(_hostingEnvironment.WebRootPath, "Plugins"),
                    SearchOption.AllDirectories);
            });
            // FINE

            //Register CORS Middlewere
            //.WithOrigins("http://localhost:4200/")
            services.AddCors(options =>
            {
                options.AddPolicy(name: "myPolicy",
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            // AGGIUNTO CORS
            /*app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });*/
            app.UseCors("myPolicy");
            // FINE

            app.UseAuthentication();

            app.UseJwtTokenMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AbpCommonHub>("/signalr");
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });

            // AGGIUNTO SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "RtlSdr API V1");
            });
            // FINE
        }
    }
}
