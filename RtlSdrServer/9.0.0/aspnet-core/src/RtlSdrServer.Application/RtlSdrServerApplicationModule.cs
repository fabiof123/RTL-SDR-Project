using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RtlSdrServer.Authorization;

namespace RtlSdrServer
{
    [DependsOn(
        typeof(RtlSdrServerCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class RtlSdrServerApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<RtlSdrServerAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(RtlSdrServerApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
