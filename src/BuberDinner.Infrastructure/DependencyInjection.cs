using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Application.Common.Interfaces.Services;
using BuberDinner.Infrastructure.Authentication;
using BuberDinner.Infrastructure.Persistence;
using BuberDinner.Infrastructure.Persistence.Interceptors;
using BuberDinner.Infrastructure.Persistence.Repositories;
using BuberDinner.Infrastructure.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuberDinner.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddAuth(configuration)
                .AddPersistance();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();



            return services;
        }
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddDbContext<BuberDinnerDbContext>(options =>
            {

                //options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BuberDinner;Integrated Security=True;Encrypt=false");
                options.UseSqlServer("Server=localhost;Database=BuberDinner;User Id=sa;Password=urmat82!;Encrypt=false");
                
            });
            services.AddScoped<PublishDomainEventsInterceptor>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IDinnerRepository, DinnerRepository>();
            return services;
        }
        public static IServiceCollection AddAuth(this IServiceCollection services, ConfigurationManager configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(JwtSettings.SectionName, jwtSettings);
            services.AddSingleton(Options.Create(jwtSettings));
            services.Configure<JwtSettings>(configuration.GetSection(JwtSettings.SectionName));

            services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }
             
    }
}
