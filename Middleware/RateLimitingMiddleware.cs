using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

public static class RateLimitingMiddleware
{
    public static void AddRateLimiting(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("fixed", limiterOptions =>
            {
                limiterOptions.PermitLimit = 10; // Máximo de 10 requisições
                limiterOptions.Window = TimeSpan.FromSeconds(10); 
                limiterOptions.QueueLimit = 2; 
            });
        });
    }
}