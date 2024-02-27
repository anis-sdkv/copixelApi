using System.Net;
using CopixelApi;
using CopixelApi.Api;
using CopixelApi.Api.Utils;
using CopixelApi.Data.Repositories;
using CopixelApi.Domain.Models;
using CopixelApi.Domain.Repositories;
using CopixelApi.Services;
using CopixelApi.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNamingPolicy = new SnakeCaseNamingPolicy(); });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentityCore<User>();
builder.Services.AddScoped<IUserStore<User>, AppUserStore>();
builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Events.OnRedirectToAccessDenied =
            Utils.ReplaceRedirector(HttpStatusCode.Forbidden, options.Events.OnRedirectToAccessDenied);
        options.Events.OnRedirectToLogin =
            Utils.ReplaceRedirector(HttpStatusCode.Unauthorized, options.Events.OnRedirectToLogin);
        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
    });

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IArtRepository, ArtRepository>();
builder.Services.AddScoped<IAccountService, AccountService>();


builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();