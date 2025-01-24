using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService; 
        }
        [HttpGet("test")]
        public IActionResult Get() {
            return Ok("Test");
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var registerResult=_authenticationService.Register(request.FirstName,request.LastName,request.Email,request.Password);
            return registerResult.Match(
                authRestul => Ok(MapAuthResult(authRestul)),
                error => Problem(statusCode: error.StatusCode,title:error.ErrorMessage));
            

            
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authRestul)
        {
            return new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request) {
            var authRestul = _authenticationService.Login(request.Email, request.Password);
            var response = new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);

            return Ok(response);
        }
    }
}
