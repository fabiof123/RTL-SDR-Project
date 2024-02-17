using Abp.Application.Services;
using RtlSdrServer.MultiTenancy.Dto;

namespace RtlSdrServer.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

