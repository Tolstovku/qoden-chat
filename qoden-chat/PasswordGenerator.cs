using Microsoft.AspNetCore.Identity;
using qoden_chat.src.Database.Entities;

namespace qoden_chat
{
    public static class PasswordGenerator
    {
        public static string HashPassword(string password)
        {
            return new PasswordHasher<User>().HashPassword(null, password);
            
        }

        public static PasswordVerificationResult VerifyPassword(string providedPassword, string hashedPassword)
        {
            return new PasswordHasher<User>().VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}