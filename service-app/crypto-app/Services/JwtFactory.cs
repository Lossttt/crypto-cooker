using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using crypto_app.Config.Options;
using crypto_app.Core.Entities.Users;
using crypto_app.Infrastructure.Repositories.Interfaces;
using crypto_app.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

public class JwtFactory : IJwtFactory
{
    private readonly IJwtTokenHandler _jwtTokenHandler;
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<JwtFactory> _logger;

    public JwtFactory(IJwtTokenHandler jwtTokenHandler, IOptions<JwtOptions> jwtOptions, ILogger<JwtFactory> logger)
    {
        _jwtTokenHandler = jwtTokenHandler;
        _jwtOptions = jwtOptions.Value;
        _logger = logger;
    }

    public async Task<string> GenerateEncodedToken(ApplicationUser user, User appUser)
    {
        if (string.IsNullOrEmpty(user.Email))
        {
            throw new ArgumentNullException(nameof(user.Email), "User email cannot be null or empty");
        }

        if (user.Id == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(user.Id), "User ID cannot be empty");
        }

        // Check if the Secret is null or empty before proceeding
        if (string.IsNullOrEmpty(_jwtOptions.Secret))
        {
            throw new ArgumentNullException(nameof(_jwtOptions.Secret), "JWT Secret cannot be null or empty");
        }

        var claims = new[] 
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.ValidIssuer,
            audience: _jwtOptions.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtOptions.ExpiresInMinutes),
            signingCredentials: creds
        );

        return _jwtTokenHandler.WriteToken(token);
    }
}
