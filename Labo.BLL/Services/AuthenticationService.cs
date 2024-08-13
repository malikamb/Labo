using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Labo.DL.Entities;
using Labo.Utils.Password;
using System.Security.Authentication;

namespace Labo.BLL.Services
{
    public class AuthenticationService(IUserRepository userRepository) : IAuthenticationService
    {
        public UserDTO Login(LoginDTO dto)
        {
            User? user = userRepository.FindOne(u => !u.IsDeleted && (u.Username.Equals(dto.Username, StringComparison.CurrentCultureIgnoreCase) || u.Email.Equals(dto.Username, StringComparison.CurrentCultureIgnoreCase)));
            if (user is null || !PasswordUtils.VerifyPassword(user.EncodedPassword, dto.Password, user.Salt))
            {
                throw new AuthenticationException();
            }
            return new UserDTO(user);
        }
    }
}
