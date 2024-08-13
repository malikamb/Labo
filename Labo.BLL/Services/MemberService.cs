using Labo.BLL.DTO.Users;
using Labo.BLL.Interfaces;
using Labo.BLL.Mappers;
using Labo.DL.Entities;
using Labo.DL.Enums;
using Labo.Utils.Password;
using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using System.Transactions;

namespace Labo.BLL.Services
{
    public class MemberService(IMailer mailer, IUserRepository userRepository) : IMemberService
    {
        private readonly string RegisterMailTemplate = @"
            <h1>Your registration for Mr Checkmate's plateform</h1>
            <div>
                <p>Your Credentials:</p>
                <p>username: __username__</p>
                <p>password: __password__</p>
            </div>
        ";

        public async Task AddAsync(MemberFormDTO dto)
        {
            if (userRepository.Any(u => u.Username.Equals(dto.Username, StringComparison.CurrentCultureIgnoreCase)))
            {
                throw new ValidationException("This username already exists") { Source = nameof(dto.Username) };
            }
            if (userRepository.Any(u => u.Email.ToLower() == dto.Email.ToLower()))
            {
                throw new ValidationException("This email already exists") { Source = nameof(dto.Email) };
            }
            string password = PasswordUtils.Random(8);
            User u = dto.ToEntity();
            u.Elo = dto.Elo ?? 1200;
            u.Role = UserRole.Player;
            u.Salt = Guid.NewGuid();
            u.EncodedPassword = PasswordUtils.HashPassword(password, u.Salt);
            u.IsDeleted = false;


            using TransactionScope t = new(TransactionScopeAsyncFlowOption.Enabled);
            {
                userRepository.Add(u);
                await mailer.SendAsync(
                    "New Registration",
                    RegisterMailTemplate
                        .Replace("__username__", u.Username)
                        .Replace("__password__", password),
                    u.Email // replaced in debug
                );
            }
            t.Complete();
        }



        public void ChangePassword(Guid id, ChangePasswordDTO dto)
        {
            User? user = userRepository.FindOne(id);
            if (user is null || !PasswordUtils.VerifyPassword(user.EncodedPassword, dto.OldPassword, user.Salt))
            {
                throw new AuthenticationException();
            }
            user.EncodedPassword = PasswordUtils.HashPassword(dto.Password, user.Salt);
            userRepository.Update(user);
        }

        public bool ExistsEmail(ExistsEmailDTO dto)
        {
            return userRepository.Any(u => u.Email.Equals(dto.Email, StringComparison.CurrentCultureIgnoreCase) && u.Id != dto.ExcludeId);
        }

        public bool ExistsUsername(ExistsUsernameDTO dto)
        {
            return userRepository.Any(u => u.Username.Equals(dto.Username, StringComparison.CurrentCultureIgnoreCase) && u.Id != dto.ExcludeId);
        }
    }
}
