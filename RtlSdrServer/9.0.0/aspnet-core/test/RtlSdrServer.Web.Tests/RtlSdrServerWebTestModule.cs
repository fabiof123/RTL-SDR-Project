using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using RtlSdrServer.EntityFrameworkCore;
using RtlSdrServer.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace RtlSdrServer.Web.Tests
{
    [DependsOn(
        typeof(RtlSdrServerWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class RtlSdrServerWebTestModule : AbpModule
    {
        public RtlSdrServerWebTestModule(RtlSdrServerEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(RtlSdrServerWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(RtlSdrServerWebMvcModule).Assembly);
        }
    }
}