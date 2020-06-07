using Microsoft.AspNetCore.Authorization;

namespace AirTasker.Showcase.RateLimit.Authorization
{
    public class RateLimitRequirement : IAuthorizationRequirement
    {
        public int RateLimit { get; }
        public double Interval { get; }

        public RateLimitRequirement(int maxRateLimit, double intervalInSecond)
        {
            RateLimit = maxRateLimit;
            Interval = intervalInSecond;
        }
    }
}
