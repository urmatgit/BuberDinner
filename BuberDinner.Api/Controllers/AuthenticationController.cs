using BuberDinner.Application.Services.Authentication;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{
 
    [Route("auth")]
    public class AuthenticationController : ApiController
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
            ErrorOr<AuthenticationResult> authRestul = _authenticationService.Register(request.FirstName,request.LastName,request.Email,request.Password);
             
            return authRestul.Match(
                authRestul => Ok(MapAuthResult(authRestul)),
                Errors => Problem(Errors));
            

            
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authRestul)
        {
            return new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request) {
            var authRestul = _authenticationService.Login(request.Email, request.Password);
            if (authRestul.IsError && authRestul.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authRestul.FirstError.Description);
            }
            return authRestul.Match(
                authResult => Ok(MapAuthResult(authResult)),
                errors=>Problem(errors)
                );
            //var response = new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);

            //return Ok(response);
        }
    }
}
