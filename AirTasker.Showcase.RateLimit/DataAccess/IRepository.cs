using System;
using System.Collections.Generic;

namespace AirTasker.Showcase.RateLimit.DataAccess
{
    public interface IRepository
    {
        void AddUserLog(string userId, DateTime nowutc);
        List<UserLog> GetUserLogsWithinInterval(string userId, double IntervalInSeconds, DateTime date);
    }
}
