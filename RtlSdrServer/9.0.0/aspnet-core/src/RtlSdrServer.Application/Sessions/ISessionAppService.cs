using System.Threading.Tasks;
using Abp.Application.Services;
using RtlSdrServer.Sessions.Dto;

namespace RtlSdrServer.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
