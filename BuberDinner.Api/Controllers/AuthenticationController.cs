using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Application.Services.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{

    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        
        public AuthenticationController(
                ISender mediator
            )
        {
            _mediator = mediator;
         
        }
        [HttpGet("test")]
        public IActionResult Get() {
            return Ok("Test");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command=new RegisterCommand(request.FirstName, request.LastName,request.Email,request.Password);

            ErrorOr<AuthenticationResult> authRestul =await _mediator.Send(command);
                //_authenticationCommandService.Register(request.FirstName,request.LastName,request.Email,request.Password);
             
            
            return authRestul.Match(
                authRestul => Ok(MapAuthResult(authRestul)),
                Errors => Problem(Errors));
            

            
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authRestul)
        {
            return new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);
        }

        [HttpPost("login")]
        public async  Task<IActionResult> Login(LoginRequest request) {

            var query=new LoginQuery(request.Email,request.Password); 

            var authRestul =await  _mediator.Send(query);
                //_authenticationQueryService.Login(request.Email, request.Password);
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
