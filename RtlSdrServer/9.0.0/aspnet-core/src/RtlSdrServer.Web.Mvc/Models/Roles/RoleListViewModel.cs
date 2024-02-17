using System.Collections.Generic;
using RtlSdrServer.Roles.Dto;

namespace RtlSdrServer.Web.Models.Roles
{
    public class RoleListViewModel
    {
        public IReadOnlyList<PermissionDto> Permissions { get; set; }
    }
}
