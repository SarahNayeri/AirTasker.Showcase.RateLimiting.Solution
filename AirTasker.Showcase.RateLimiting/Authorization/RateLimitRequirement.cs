using Microsoft.AspNetCore.Authorization;

namespace AirTasker.Showcase.RateLimiting.Authorization
{
    public class RateLimitRequirement : IAuthorizationRequirement
    {
        public int RateLimit { get; }
        public int Interval { get; }

        public RateLimitRequirement(int maxRateLimit, int intervalInSecond)
        {
            RateLimit = maxRateLimit;
            Interval = intervalInSecond;
        }
    }
}
