﻿using BuberDinner.Application.Common.Interfaces.Authentication;
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

        public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
        }
        public AuthenticationResult Login(string email, string password)
        {
            return new AuthenticationResult(Guid.NewGuid(),"firstName","lastName" ,email,"token");
        }

        public AuthenticationResult Register(string firstName, string lastName, string email, string password)
        {
            //check user if alreade exists

            //create user

            // create Jwt token
            Guid userId= Guid.NewGuid();
            var token=_jwtTokenGenerator.GeneratorToken(userId,firstName,lastName);
            return new AuthenticationResult(userId, firstName, lastName, email, token);
        }
    }
}
