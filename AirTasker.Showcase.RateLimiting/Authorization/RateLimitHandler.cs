using AirTasker.Showcase.RateLimiting.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AirTasker.Showcase.RateLimiting.Authorization
{
    public class RateLimitHandler : AuthorizationHandler<RateLimitRequirement>
    {
        private readonly IRepository _repository;

        public RateLimitHandler(IRepository repository)
        {
            _repository = repository;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RateLimitRequirement requirement)
        {
            context.Succeed(requirement);
            return Task.FromResult($"Rate limit exceeded.Try again in {100} seconds");

            //var waitingTime = _repository.CalculateWaitingTime(context.User.Identity.Name, requirement.RateLimit, requirement.Interval);
            //if (waitingTime > 0)
            //{
            //    context.Fail();
            //    return Task.FromResult($"Rate limit exceeded.Try again in {waitingTime} seconds");
            //}
            //else
            //{
            //    context.Succeed(requirement);
            //    return Task.CompletedTask;
            //}
        }
    }
}
