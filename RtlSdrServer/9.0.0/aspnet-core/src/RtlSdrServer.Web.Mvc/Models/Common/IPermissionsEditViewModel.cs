using System.Collections.Generic;
using RtlSdrServer.Roles.Dto;

namespace RtlSdrServer.Web.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }
    }
}