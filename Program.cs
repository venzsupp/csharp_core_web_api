using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
// using Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.Data.SqlClient;
using csharp_core_web_api.Abstracts;
using csharp_core_web_api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using csharp_core_web_api.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var connectionString = builder.Configuration.GetConnectionString("StudentDbConnection");

Console.WriteLine($"connectionString, {connectionString}");

Action<DbContextOptionsBuilder> dbOptions = options =>
    options.UseSqlServer(connectionString);



builder.Services.AddDbContext<UserDbContext>(dbOptions);
builder.Services.AddDbContext<StudentDbContext>(dbOptions);
builder.Services.AddDbContext<OAuthCredentialsTokenDbContext>(dbOptions);

var configSection = builder.Configuration.GetSection("OAuthCredentials");
Console.WriteLine($"ClientID: {configSection["ClientID"]}");
Console.WriteLine($"Domain: {configSection["Domain"]}");

// builder.Services.AddOptions<OAuthCredentials>().BindConfiguration("OAuthCredentials");
// builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<OAuthCredentials>>().Value);

builder.Services.Configure<OAuthCredentials>(
    builder.Configuration.GetSection("OAuthCredentials"));

builder.Services.Configure<OauthAuthorizationCode>(
    builder.Configuration.GetSection("OauthAuthorizationCode"));


// builder.Services.AddOptions<MyTestClass>().BindConfiguration("MyTestClass");
// builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MyTestClass>>().Value);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();
app.UseMiddleware<OAuthMiddleware>();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
