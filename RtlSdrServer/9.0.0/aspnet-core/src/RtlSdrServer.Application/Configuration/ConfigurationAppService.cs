using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using RtlSdrServer.Configuration.Dto;

namespace RtlSdrServer.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : RtlSdrServerAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
