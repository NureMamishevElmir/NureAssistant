using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainEntity.CustomerEntities;
using Identity.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Services.Impl;

/// <inheritdoc cref="ITokenService"/>
public class TokenService : ITokenService
{
    private readonly JwtTokenSettings _settings;

    public TokenService(IOptions<JwtTokenSettings> settings)
    {
        _settings = settings.Value;
    }

    public (string Token, DateTime ValidTo) Generate(Customer customer)
    {
        var now = DateTime.UtcNow;
        var expires = now.AddMinutes(_settings.TokenLifespanInMinutes);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, customer.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, customer.Id.ToString()),
            new(ClaimTypes.Role, customer.Type.ToString()),
        };

        if (!string.IsNullOrWhiteSpace(customer.Email))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, customer.Email));
            claims.Add(new Claim(ClaimTypes.Name, customer.Email));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _settings.Issuer,
            audience: _settings.Audience,
            claims: claims,
            notBefore: now,
            expires: expires,
            signingCredentials: credentials);

        var encoded = new JwtSecurityTokenHandler().WriteToken(token);

        return (encoded, expires.ToLocalTime());
    }
}
