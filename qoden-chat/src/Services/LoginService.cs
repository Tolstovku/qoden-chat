using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using qoden_chat.src.Database;
using qoden_chat.src.Models.Requests;
using Qoden.Validation;

namespace qoden_chat.src.Services
{
    public interface ILoginService
    {
        Task<ClaimsPrincipal> Login(LoginRequest request);
    }

    public class LoginService : ILoginService
    {
        private readonly DatabaseContext _db;
        private const string invalidLoginMsg = "Wrong username or password";

        public LoginService(DatabaseContext db)
        {
            _db = db;
        }

        public async Task<ClaimsPrincipal> Login(LoginRequest req)
        {
            var user = await _db.Users.SingleOrDefaultAsync(u => u.Username == req.Username);
            Check.Value(user).NotNull(invalidLoginMsg);
            var verificationResult = PasswordGenerator.VerifyPassword(req.Password, user.Password);
            Check.Value(verificationResult)
                .EqualsTo(PasswordVerificationResult.Success, invalidLoginMsg);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            return new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
        }
    }
}