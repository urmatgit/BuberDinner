using BuberDinner.Application.Common.Behaviors;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Commands.Register;
using BuberDinner.Application.Services.Authentication.Common;
using BuberDinner.Application.Services.Authentication.Queries;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
            //services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();
            //services.AddMediatR(typeof(DependencyInjection).Assembly);
            services.AddMediatR(cfg=>cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
