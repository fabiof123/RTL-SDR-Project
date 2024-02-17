using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RtlSdrServer.Configuration;

namespace RtlSdrServer.Web.Startup
{
    [DependsOn(typeof(RtlSdrServerWebCoreModule))]
    public class RtlSdrServerWebMvcModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public RtlSdrServerWebMvcModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<RtlSdrServerNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RtlSdrServerWebMvcModule).GetAssembly());
        }
    }
}
