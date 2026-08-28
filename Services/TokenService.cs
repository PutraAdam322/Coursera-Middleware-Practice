using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.JsonWebTokens;

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

    public string GenerateToken(User user)
    {
        DotNetEnv.Env.Load();
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            //new Claim(ClaimTypes.Role, user.Role) // Assuming User has a Role property
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET_KEY"))) ?? throw new ArgumentNullException("JWT_SECRET_KEY is not set");
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
}