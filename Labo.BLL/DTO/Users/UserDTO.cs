using Labo.DL.Entities;
using Labo.DL.Enums;

namespace Labo.BLL.DTO.Users
{
    public class UserDTO(User user)
    {
        public Guid Id { get; set; } = user.Id;
        public string Username { get; set; } = user.Username;
        public string Email { get; set; } = user.Email;
        public DateTime BirthDate { get; set; } = user.BirthDate;
        public int Elo { get; set; } = user.Elo;
        public UserGender Gender { get; set; } = user.Gender;
        public UserRole Role { get; set; } = user.Role;
    }
}
