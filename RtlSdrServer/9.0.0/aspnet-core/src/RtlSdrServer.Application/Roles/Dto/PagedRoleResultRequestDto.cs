using Abp.Application.Services.Dto;

namespace RtlSdrServer.Roles.Dto
{
    public class PagedRoleResultRequestDto : PagedResultRequestDto
    {
        public string Keyword { get; set; }
    }
}

