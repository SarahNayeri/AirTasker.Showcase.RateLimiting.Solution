using AirTasker.Showcase.RateLimit.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleToAttribute("AirTasker.Showcase.RateLimit.XUnitTest")]
namespace AirTasker.Showcase.RateLimit.Authorization
{
    public class RateLimitHandler : AuthorizationHandler<RateLimitRequirement>
    {
        private readonly IRateLimitService _rateLimitService;
        private readonly IHttpContextAccessor _accessor;
        private readonly IDateTimeService _dateTimeService;

        public RateLimitHandler(IRateLimitService rateLimitService, IHttpContextAccessor accessor, IDateTimeService dateTimeService)
        {
            _rateLimitService = rateLimitService;
            _accessor = accessor;
            _dateTimeService = dateTimeService;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RateLimitRequirement requirement)
        {
            var utcnow = _dateTimeService.GetUTC();
            var waitingtime = _rateLimitService.GetWaitingTime(context.User.Identity.Name, requirement.RateLimit, requirement.Interval, utcnow);
            if (waitingtime > 0)
            {
                context.Fail();
                _accessor.HttpContext.Response.StatusCode = 429;
                _accessor.HttpContext.Response.Body.WriteAsync(
                    Encoding.UTF8.GetBytes($"Rate limit exceeded. Try again in #{waitingtime} seconds"));
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
