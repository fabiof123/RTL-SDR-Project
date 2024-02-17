using Abp.AutoMapper;
using RtlSdrServer.Sessions.Dto;

namespace RtlSdrServer.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}
