using System;

namespace AirTasker.Showcase.RateLimit.Service
{
    public interface IRateLimitService
    {
        double GetWaitingTime(string userId, int rateLimit, double IntervalInSeconds, DateTime date);
        void AddUserLog(string userId, DateTime nowutc);
    }
}
