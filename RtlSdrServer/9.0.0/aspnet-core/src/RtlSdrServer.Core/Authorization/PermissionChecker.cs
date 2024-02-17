using Abp.Authorization;
using RtlSdrServer.Authorization.Roles;
using RtlSdrServer.Authorization.Users;

namespace RtlSdrServer.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
