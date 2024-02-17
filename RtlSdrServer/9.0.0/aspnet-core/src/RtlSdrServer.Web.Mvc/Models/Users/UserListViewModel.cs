using System.Collections.Generic;
using RtlSdrServer.Roles.Dto;

namespace RtlSdrServer.Web.Models.Users
{
    public class UserListViewModel
    {
        public IReadOnlyList<RoleDto> Roles { get; set; }
    }
}
