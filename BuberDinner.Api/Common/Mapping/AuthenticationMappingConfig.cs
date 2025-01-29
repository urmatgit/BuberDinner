using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries.Login;
using BuberDinner.Contracts.Authentication;
using Mapster;
using Microsoft.AspNetCore.Authentication;

namespace BuberDinner.Api.Common.Mapping
{
    public class AuthenticationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequest, RegisterCommand>();

            config.NewConfig<LoginRequest, LoginQuery>();
             
            config.NewConfig<AuthenticationResult, AuthenticationResponse>()
             .Map(dest => dest, src => src.User);
        }
    }
}
