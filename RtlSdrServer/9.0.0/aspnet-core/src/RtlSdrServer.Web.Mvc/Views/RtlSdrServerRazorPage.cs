using Abp.AspNetCore.Mvc.Views;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;

namespace RtlSdrServer.Web.Views
{
    public abstract class RtlSdrServerRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject]
        public IAbpSession AbpSession { get; set; }

        protected RtlSdrServerRazorPage()
        {
            LocalizationSourceName = RtlSdrServerConsts.LocalizationSourceName;
        }
    }
}
