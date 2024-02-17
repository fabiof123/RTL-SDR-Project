using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RtlSdrServer.Configuration;

namespace RtlSdrServer.Web.Host.Startup
{
    [DependsOn(
       typeof(RtlSdrServerWebCoreModule))]
    public class RtlSdrServerWebHostModule: AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public RtlSdrServerWebHostModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RtlSdrServerWebHostModule).GetAssembly());
        }
    }
}
