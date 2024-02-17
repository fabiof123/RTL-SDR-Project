using System.ComponentModel.DataAnnotations;

namespace RtlSdrServer.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}