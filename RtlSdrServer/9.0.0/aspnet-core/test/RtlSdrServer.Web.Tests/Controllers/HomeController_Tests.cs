using System.Threading.Tasks;
using RtlSdrServer.Models.TokenAuth;
using RtlSdrServer.Web.Controllers;
using Shouldly;
using Xunit;

namespace RtlSdrServer.Web.Tests.Controllers
{
    public class HomeController_Tests: RtlSdrServerWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}