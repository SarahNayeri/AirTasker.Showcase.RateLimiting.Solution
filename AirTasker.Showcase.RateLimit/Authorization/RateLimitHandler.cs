using AirTasker.Showcase.RateLimit.Service;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace AirTasker.Showcase.RateLimit.Authorization
{
    public class RateLimitHandler : AuthorizationHandler<RateLimitRequirement>
    {
        private readonly IRateLimitService _rateLimitService;

        public RateLimitHandler(IRateLimitService rateLimitService)
        {
            _rateLimitService = rateLimitService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RateLimitRequirement requirement)
        {
            var utcnow = DateTime.UtcNow;
            _rateLimitService.SetRateLimit(context.User.Identity.Name, requirement.RateLimit, requirement.Interval, utcnow);
            if (_rateLimitService.GetWaitingTime(context.User.Identity.Name) > 0)
            {
                context.Fail();
            }
            else
            {
                _rateLimitService.AddUserLog(context.User.Identity.Name, utcnow);
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
