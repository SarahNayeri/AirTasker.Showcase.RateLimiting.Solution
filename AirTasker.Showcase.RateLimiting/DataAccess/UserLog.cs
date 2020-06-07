using System;

namespace AirTasker.Showcase.RateLimiting.DataAccess
{
    public class UserLog
    {
        public string UserId { get; set; }
        public DateTime RequestTime { get => DateTime.UtcNow; }
    }
}
