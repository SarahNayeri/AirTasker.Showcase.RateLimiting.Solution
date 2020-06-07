using System;

namespace AirTasker.Showcase.RateLimit.DataAccess
{
    public class UserLog
    {
        public Guid LogId { get; set; }
        public string UserId { get; set; }
        public DateTime RequestTime { get; set; }
    }
}
