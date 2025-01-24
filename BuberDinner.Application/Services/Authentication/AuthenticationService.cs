using BuberDinner.Application.Common.Errors;
using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Common.Errors;
using BuberDinner.Domain.Entities;
using ErrorOr;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;
        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator,IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }
        public ErrorOr<AuthenticationResult> Login(string email, string password)
        {
            //1. validate user exists
            if (_userRepository.GetUserByEmail(email) is not User user)
            {
                return Errors.Authentication.InvalidCredentials;
            }
            if (user.Password != password) {
                return Errors.Authentication.InvalidCredentials;
            }
            var token = _jwtTokenGenerator.GeneratorToken(user);

            return new AuthenticationResult(user,token);
        }

        public ErrorOr<AuthenticationResult> Register(string firstName, string lastName, string email, string password)
        {
            //check user if alreade exists
            if (_userRepository.GetUserByEmail(email) is not null) {
                //throw new Exception("User with given email already exists."); //new DublicateEmailException();
                return  Errors.User.DublicateEmail;
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
            Guid userId= user.Id;
            var token=_jwtTokenGenerator.GeneratorToken(user);
            return new AuthenticationResult(user, token);
        }
    }
}
