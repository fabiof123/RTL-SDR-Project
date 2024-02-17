using Abp.AutoMapper;
using RtlSdrServer.Roles.Dto;
using RtlSdrServer.Web.Models.Common;

namespace RtlSdrServer.Web.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class EditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool HasPermission(FlatPermissionDto permission)
        {
            return GrantedPermissionNames.Contains(permission.Name);
        }
    }
}
