using BoatBooking.API.Middleware;
using BoatBooking.Application.Commands.ApproveUser;
using BoatBooking.Application.Commands.LoginUser;
using BoatBooking.Application.Commands.RegisterUser;
using BoatBooking.Application.Commands.RejectUser;
using BoatBooking.Application.Interfaces;
using BoatBooking.Application.Queries.GetPendingOwners;
using BoatBooking.Infrastructure.Data;
using BoatBooking.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add controllers with JSON options to handle enum serialization as strings
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter());
    });

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// DI for application services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRegisterUserCommand, RegisterUserCommandHandler>();
builder.Services.AddScoped<ILoginUserCommand, LoginUserCommandHandler>();
builder.Services.AddScoped<IGetPendingOwnersQuery, GetPendingOwnersQueryHandler>();
builder.Services.AddScoped<IApproveOwnerCommand, ApproveOwnerCommandHandler>();
builder.Services.AddScoped<IRejectOwnerCommand, RejectOwnerCommand>();

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
    };
});

builder.Services.AddAuthorization();

// logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(
        "Logs/log-.txt",
        rollingInterval: RollingInterval.Month
    )
    .CreateLogger();

builder.Host.UseSerilog();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Global exception handling
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
