using System;
using Microsoft.AspNetCore.Builder;

var users = new List<User>{};
var posts = new List<Post>{};

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

var app = builder.Build();

app.Run();
