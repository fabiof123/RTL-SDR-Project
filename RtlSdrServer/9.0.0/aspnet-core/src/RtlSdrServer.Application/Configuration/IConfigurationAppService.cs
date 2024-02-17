using System.Threading.Tasks;
using RtlSdrServer.Configuration.Dto;

namespace RtlSdrServer.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
