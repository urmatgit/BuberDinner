using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using BuberDinner.Application.Services.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using BuberDinner.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuberDinner.Api.Controllers
{

    [Route("auth")]
    public class AuthenticationController : ApiController
    {
        private readonly ISender _mediator;
        private readonly IMapper _mapper;
        public AuthenticationController(
                ISender mediator,
                IMapper mapper
            )
        {
            _mediator = mediator;
         _mapper = mapper;
        }
        [HttpGet("test")]
        public IActionResult Get() {
            return Ok("Test");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command=_mapper.Map<RegisterCommand>(request);

            ErrorOr<AuthenticationResult> authRestul =await _mediator.Send(command);
                //_authenticationCommandService.Register(request.FirstName,request.LastName,request.Email,request.Password);
             
            
            return authRestul.Match(
                authRestul => Ok(_mapper.Map<AuthenticationResponse>(authRestul)),
                Errors => Problem(Errors));
            

            
        }

        private static AuthenticationResponse MapAuthResult(AuthenticationResult authRestul)
        {
            return new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);
        }

        [HttpPost("login")]
        public async  Task<IActionResult> Login(LoginRequest request) {

            var query=_mapper.Map <LoginQuery>(request); 

            var authRestul =await  _mediator.Send(query);
                //_authenticationQueryService.Login(request.Email, request.Password);
            if (authRestul.IsError && authRestul.FirstError == Errors.Authentication.InvalidCredentials)
            {
                return Problem(
                    statusCode: StatusCodes.Status401Unauthorized,
                    title: authRestul.FirstError.Description);
            }
            return authRestul.Match(
                authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
                errors=>Problem(errors)
                );
            //var response = new AuthenticationResponse(authRestul.User.Id, authRestul.User.FirstName, authRestul.User.LastName, authRestul.User.Email, authRestul.Token);

            //return Ok(response);
        }
    }
}
