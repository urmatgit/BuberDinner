namespace BuberDinner.Application.Services.Authentication.Commands.Register
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using BuberDinner.Application.Common.Interfaces.Authentication;
    using BuberDinner.Application.Common.Interfaces.Persistence;
    using BuberDinner.Application.Services.Authentication.Common;
    using BuberDinner.Domain.Common.Errors;
    using BuberDinner.Domain.Entities;
    using ErrorOr;
    using MediatR;

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ErrorOr<AuthenticationResult>>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }


    

        public async Task<ErrorOr<AuthenticationResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {

            await Task.CompletedTask;
            //check user if alreade exists
            if (_userRepository.GetUserByEmail(request.Email) is not null)
            {
                //throw new Exception("User with given email already exists."); //new DublicateEmailException();
                return  Errors.User.DublicateEmail;
            }

            //create user
            var user = User.Create(request.FirstName,request.LastName,request.Email,request.Password);
            
            _userRepository.AddUser(user);
            // create Jwt token
            Guid userId = user.Id.Value;
            var token = _jwtTokenGenerator.GeneratorToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
