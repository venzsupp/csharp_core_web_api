using System;
namespace csharp_core_web_api.Middleware;
public class OAuthMiddleware
{
    private readonly RequestDelegate _next;

    public OAuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Request incoming");
        Console.WriteLine($"Context {context.Request.Headers["Authorization"]}");
        // Get local date and time
        DateTime localNow = DateTime.Now; 

        // Get UTC date and time (Best practice for databases)
        DateTime utcNow = DateTime.UtcNow;

        // Convert to a specific string format
        string formattedTime = localNow.ToString("yyyy-MM-dd HH:mm:ss");

        Console.WriteLine($"Local: {localNow}");
        Console.WriteLine($"UTC: {utcNow}");
        Console.WriteLine($"Formatted: {formattedTime}");
        // int result = formattedTime.CompareTo(expiry);
        // if (result > 0)
        // {

        // }
        await _next(context);
        Console.WriteLine("Request out going");
    }
}