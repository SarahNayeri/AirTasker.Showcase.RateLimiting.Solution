using AirTasker.Showcase.RateLimit.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AirTasker.Showcase.RateLimit.Authorization
{
    public class RateLimitSchemeHandler : IAuthenticationHandler
    {

        private HttpContext _context;
        private readonly IRateLimitService _rateLimitService;
        public RateLimitSchemeHandler(IRateLimitService rateLimitService)
        {
            _rateLimitService = rateLimitService;
        }
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _context = context;
            return Task.CompletedTask;
        }

        public Task<AuthenticateResult> AuthenticateAsync()
            => Task.FromResult(AuthenticateResult.NoResult());

        public Task ChallengeAsync(AuthenticationProperties properties)
          => Task.FromResult(AuthenticateResult.NoResult());

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            var waitingtime = _rateLimitService.GetWaitingTime(_context.User.Identity.Name);
            _context.Response.StatusCode = 429;
            var jsonString = $"you need to wait for {waitingtime} seconds.";
            _context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            _context.Response.WriteAsync(jsonString, Encoding.UTF8);
            return Task.FromResult(AuthenticateResult.NoResult());
        }
    }
}
