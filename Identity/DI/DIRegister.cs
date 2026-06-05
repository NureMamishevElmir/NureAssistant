using DomainEntity.CustomerEntities;
using Identity.Configuration;
using Identity.Services;
using Identity.Services.Impl;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.DI;

public static class DIRegister
{
    public static IServiceCollection ConfigureIdentityDI(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtTokenSettings>(configuration.GetSection("JwtTokenSettings"));

        services.AddScoped<IPasswordHasher<Customer>, PasswordHasher<Customer>>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
}
