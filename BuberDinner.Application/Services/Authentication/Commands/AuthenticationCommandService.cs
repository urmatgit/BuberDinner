using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication.Commands
{
    public class AuthenticationCommandService : IAuthenticationCommandService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationCommandService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //check user if alreade exists
            if (_userRepository.GetUserByEmail(email) is not null)
            {
                //throw new Exception("User with given email already exists."); //new DublicateEmailException();
                return Errors.User.DublicateEmail;
            }

            //create user
            var user = new User
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };
            _userRepository.AddUser(user);
            // create Jwt token
            Guid userId = user.Id;
            var token = _jwtTokenGenerator.GeneratorToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
