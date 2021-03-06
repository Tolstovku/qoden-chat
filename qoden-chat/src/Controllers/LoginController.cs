using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using qoden_chat.src.Models.Requests;
using qoden_chat.src.Services;

namespace qoden_chat.src.Controllers
{
    [Route("/api/v1/")]
    public class LoginController : Controller
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task Login([FromBody] LoginRequest req)
        {
                var claimsPrincipal = await _loginService.Login(req);
                await HttpContext.SignInAsync(claimsPrincipal);
        }


        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}