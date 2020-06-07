using System;
using System.Collections.Generic;

namespace AirTasker.Showcase.RateLimit.DataAccess
{
    public interface IRepository
    {
        void AddUserLog(string userId, DateTime nowutc);
        List<UserLog> GetUserLogs(string userId, double IntervalInSeconds, DateTime date);
    }
}
