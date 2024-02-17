using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RtlSdrServer.Configuration;
using RtlSdrServer.EntityFrameworkCore;
using RtlSdrServer.Migrator.DependencyInjection;

namespace RtlSdrServer.Migrator
{
    [DependsOn(typeof(RtlSdrServerEntityFrameworkModule))]
    public class RtlSdrServerMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public RtlSdrServerMigratorModule(RtlSdrServerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(RtlSdrServerMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                RtlSdrServerConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RtlSdrServerMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
