using System.Net;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CopixelApi.Api.Utils;

static class Utils
{
    public static Func<RedirectContext<CookieAuthenticationOptions>, Task> ReplaceRedirector(
        HttpStatusCode statusCode,
        Func<RedirectContext<CookieAuthenticationOptions>, Task> existingRedirector)
    {
        return context =>
        {
            if (!context.Request.Path.StartsWithSegments("/accounts")) return existingRedirector(context);
            context.Response.StatusCode = (int)statusCode;
            return Task.CompletedTask;
        };
    }
}