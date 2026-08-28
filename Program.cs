using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var users = new List<User>{};
var posts = new List<Post>{};

var builder = WebApplication.CreateBuilder(args);

var secretKey = builder.Configuration["JWT_SECRET_KEY"]; //

builder.Services.AddAuthorization();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
        ValidateIssuer = false, // Set to true and configure if needed
        ValidateAudience = false // Set to true and configure if needed
    };
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IUserRepositoryService, UserRepositoryService>();
builder.Services.AddSingleton<IHasherService, BCryptHasherService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddOpenApi();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async (context, next) =>
{
    try
    {
        await next();
    } catch (Exception ex)
    {
        Console.WriteLine($"Global exception caught: {ex.Message}");
        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("An exception was caught, please try again later.");
    }
});

app.UseRouting();

app.MapControllers();

app.Run();
