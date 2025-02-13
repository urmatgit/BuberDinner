using BuberDinner.Api.Common.Errors;
using BuberDinner.Api.Common.Mapping;
using BuberDinner.Application.Services.Authentication.Commands;
using BuberDinner.Application.Services.Authentication.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<ProblemDetailsFactory, BuberDinnerProblemDetailsFactory>();
            services.AddMapping();
            return services;
        }
    }
}
