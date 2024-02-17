using Abp.AspNetCore.Mvc.ViewComponents;

namespace RtlSdrServer.Web.Views
{
    public abstract class RtlSdrServerViewComponent : AbpViewComponent
    {
        protected RtlSdrServerViewComponent()
        {
            LocalizationSourceName = RtlSdrServerConsts.LocalizationSourceName;
        }
    }
}
