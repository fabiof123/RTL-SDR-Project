using Abp.AspNetCore.Configuration;
using Abp.EntityFrameworkCore.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Microsoft.EntityFrameworkCore;

namespace RtlSdrServer.Lettura.Application
{
    public class LetturaModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Modules.AbpAspNetCore().CreateControllersForAppServices(typeof(LetturaModule)
                .Assembly,
                moduleName: "app", useConventionalHttpVerbs: true);

            Configuration.Modules.AbpEfCore().AddDbContext<LetturaDbContext>(options =>
            {
                if (options.ExistingConnection is not null)
                {
                    options.DbContextOptions.UseSqlServer(options.ExistingConnection);
                }
                else
                {
                    options.DbContextOptions.UseSqlServer(options.ConnectionString);
                }
            });

            /*
            Configuration.Modules.AbpAutoMapper().Configurators.Add(x =>
            {
                x.CreateMap<PresaInCarico, PresaInCaricoDto>().ReverseMap();
            });
            */
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(LetturaModule).GetAssembly());
            /*Inversion of control*/
        }

    }
}
