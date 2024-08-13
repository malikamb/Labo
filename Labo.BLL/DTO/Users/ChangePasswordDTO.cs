using System.ComponentModel.DataAnnotations;

namespace Labo.BLL.DTO.Users
{
    public class ChangePasswordDTO
    {
        [Required]
        public string OldPassword { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
