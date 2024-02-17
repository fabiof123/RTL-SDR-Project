using Abp.AspNetCore.Mvc.Controllers;
using Abp.IdentityFramework;
using Microsoft.AspNetCore.Identity;

namespace RtlSdrServer.Controllers
{
    public abstract class RtlSdrServerControllerBase: AbpController
    {
        protected RtlSdrServerControllerBase()
        {
            LocalizationSourceName = RtlSdrServerConsts.LocalizationSourceName;
        }

        protected void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }
    }
}
