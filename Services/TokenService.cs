using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.AspNetCore.Authentication;

public class TokenService : ITokenService
{
     // Load environment variables from .env file
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void LogCreation(string message)
    {
        // Implement logging logic here, e.g., write to a file or database
        Console.WriteLine($"Token creation log: {message}");
    }

    public async Task<string> GenerateToken(User user)
    {
        var jwtSecretKey = _configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT key is not configured.");
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(ClaimTypes.Role, user.Role) // Assuming User has a Role property
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSecretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds
        };

        LogCreation($"Token generated for user: {user.Username}");

        return new JsonWebTokenHandler().CreateToken(descriptor);
    }

    public async Task<int> GetUserIdFromToken(string token)
    {
        var handler = new JsonWebTokenHandler();
        JsonWebToken tkn = handler.ReadJsonWebToken(token);
        int userId = int.Parse(tkn.Subject);
        return userId;
    }
}