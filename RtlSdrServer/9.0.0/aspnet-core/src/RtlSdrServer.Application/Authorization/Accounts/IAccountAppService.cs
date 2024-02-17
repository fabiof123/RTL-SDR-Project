using System.Threading.Tasks;
using Abp.Application.Services;
using RtlSdrServer.Authorization.Accounts.Dto;

namespace RtlSdrServer.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
