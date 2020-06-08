using System;

namespace AirTasker.Showcase.RateLimit.Service
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetUTC()
        {
            return DateTime.UtcNow;
        }
    }
}
