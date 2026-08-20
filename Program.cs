using System;
using Microsoft.AspNetCore.Builder;

var users = new List<User>{};
var posts = new List<Post>{};

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddSingleton<IUserRepositoryService, UserRepositoryService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddOpenApi();

var app = builder.Build();

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

app.MapControllers();

app.Run();
