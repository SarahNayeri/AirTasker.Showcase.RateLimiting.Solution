using System;

namespace AirTasker.Showcase.RateLimit.Service
{
    public interface IRateLimitService
    {
        void SetRateLimit(string userId, int rateLimit, double IntervalInSeconds, DateTime nowutc);
        double GetWaitingTime(string userId);
        void AddUserLog(string userId, DateTime nowutc);
    }
}
