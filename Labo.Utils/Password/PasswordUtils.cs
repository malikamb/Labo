using System.Security.Cryptography;
using System.Text;

namespace Labo.Utils.Password
{
    public static class PasswordUtils
    {
        public static byte[] HashPassword(string plainPassword, Guid salt)
        {
            return SHA512.HashData(Encoding.UTF8.GetBytes(plainPassword + salt.ToString()));
        }

        public static bool VerifyPassword(byte[] hashedPassword, string plainPassword, Guid salt)
        {
            return hashedPassword.SequenceEqual(HashPassword(plainPassword, salt));
        }

        public static string Random(int size)
        {
            StringBuilder builder = new();
            Random random = new();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            return builder.ToString();
        }
    }
}
