using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BuberDinner.Application.Common.Errors;
using FluentResults;

namespace BuberDinner.Application.Services.Authentication
{
    public interface IAuthenticationService
    {
       AuthenticationResult Login(string email, string password);
        Result<AuthenticationResult> Register( string firstName,string lastName,string email, string password);
    }
}
