using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.DTO.Users
{
    public class SearchUserDTO
    {
        [EmailAddress]
        public string? Email { get; set; } = string.Empty;
        public string? Username { get; set; } = string.Empty;
        public Guid? ExcludeId { get; set; }
    }
}
