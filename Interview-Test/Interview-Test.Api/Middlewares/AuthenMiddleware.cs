using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace Interview_Test.Middlewares;

public class AuthenMiddleware : IMiddleware
{
    private const string hashedKey = "A6F9E3C2D7B81F4E0A9C5D6B2E1F8A7C4D0E9B6F5A3C8D2E1B7F9A4C6E0D5B8A1F2C9E7D6B4A3F5E0C8D2";
    public Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var apiKeyHeader = context.Request.Headers["x-api-key"];
        if (string.IsNullOrEmpty(apiKeyHeader))
        {
            context.Response.StatusCode = 401;
            return context.Response.WriteAsync("API Key is missing");
        }

        string incomingHashedKey = HashSha512(apiKeyHeader!);
        string _hashedKey = HashSha512(hashedKey!);

        if (!string.Equals(
                incomingHashedKey,
                _hashedKey,
                StringComparison.OrdinalIgnoreCase))
        {
            context.Response.StatusCode = 401;
            return context.Response.WriteAsync("Invalid API key");
        }

        return next(context);
    }

    private string HashSha512(string value)
    {
        using var sha512 = SHA512.Create();
        byte[] bytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(value));
        return Convert.ToHexString(bytes); // 128-char hex string
    }
}