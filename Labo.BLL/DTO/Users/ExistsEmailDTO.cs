using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.DTO.Users
{
    public class ExistsEmailDTO
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; } = string.Empty;

        public Guid? ExcludeId { get; set; }
    }
}
