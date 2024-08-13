using Labo.BLL.DTO.Users;
using Labo.DL.Entities;

namespace Labo.BLL.Mappers
{
    internal static class UserMappers
    {
        public static User ToEntity(this MemberFormDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
            };
        }
    }
}
