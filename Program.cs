using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
// using Abstract;
using Microsoft.AspNetCore.Cors;
using Microsoft.Data.SqlClient;
using csharp_core_web_api.Abstracts;
using csharp_core_web_api.Models;
using Microsoft.EntityFrameworkCore;


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

    // builder.Services.AddAllDbContexts(builder.Configuration);
    Action<DbContextOptionsBuilder> dbOptions = options =>
        options.UseSqlServer(connectionString);



    builder.Services.AddDbContext<UserDbContext>(dbOptions);
    builder.Services.AddDbContext<StudentDbContext>(dbOptions);
//Plm18@1QazMeeva-- oauth--pawd
// dev-53038owxmae5eghj.au.auth0.com -- tenant domain

// cleint secret == a4pYfBgeQFntXfE2gMoOWk2hCrVWFWQ7un6fp0EQPCj06o1b463w2tcSYKogOfn3
// client id == AsvMJsJwFhnpBP0PDUm0r1aBXQcH1PVd

// builder.Services.AddSingleton<IDbConnection>( ser =>
// {
//     // var connectionStringVal1 =builder.Configuration.GetValue<string>("dbstring");
//     // Console.WriteLine("======connectionStringVal1111======");
//     // Console.WriteLine(connectionStringVal1);
//     // Console.WriteLine("======connectionStringVal111 END======");

//     var connectionStringVal = builder.Configuration.GetConnectionString("StudentDbConnection");
//     Console.WriteLine("======connectionStringVal======");
//     Console.WriteLine(connectionStringVal);
//     Console.WriteLine("======connectionStringVal END======");

//     return new SqlConnection(connectionStringVal);
//     //ser.
//     //IDbConnector.ConnectionString = connectionStringVal.StudentDbConnection;
//     // DbConnection dbConnect = new();
//     //  dbConnect.connectDB("dsfsf");
//     //return DbConnection; 
// });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowAll");

app.Run();
