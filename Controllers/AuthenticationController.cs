using EcommerceProject.TokenAuthentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenManager _tokenManager;
        public AuthenticationController(ITokenManager tokenManager)
        {
            this._tokenManager = tokenManager;
        }
        public IActionResult Authenticate(string user, string password)
        {
            if (_tokenManager.Authenticate(user, password))
            {
                return Ok(new { Token = _tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not authorized to perform this activity.");
                return Unauthorized(ModelState);
            }
        }
    }
}
