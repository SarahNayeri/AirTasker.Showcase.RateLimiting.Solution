using AirTasker.Showcase.RateLimit.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace AirTasker.Showcase.RateLimit.Authorization
{
    public class RateLimitSchemeHandler : IAuthenticationHandler
    {

        public RateLimitSchemeHandler(IRateLimitService rateLimitService)
        {
        }
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            return Task.CompletedTask;
        }

        public Task<AuthenticateResult> AuthenticateAsync()
            => Task.FromResult(AuthenticateResult.NoResult());

        public Task ChallengeAsync(AuthenticationProperties properties)
          => Task.FromResult(AuthenticateResult.NoResult());

        public Task ForbidAsync(AuthenticationProperties properties)
                      => Task.FromResult(AuthenticateResult.NoResult());
    }
}
