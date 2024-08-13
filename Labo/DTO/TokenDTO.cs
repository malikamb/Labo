using Labo.BLL.DTO.Users;

namespace Labo.API.DTO
{
    public class TokenDTO(string token, UserDTO user)
    {
        public string Token { get; set; } = token;
        public UserDTO User { get; set; } = user;
    }
}
