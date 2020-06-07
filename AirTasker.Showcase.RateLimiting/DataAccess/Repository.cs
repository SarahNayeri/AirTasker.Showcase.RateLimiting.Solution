using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AirTasker.Showcase.RateLimiting.DataAccess
{
    public class Repository : IRepository
    {
        public double CalculateWaitingTime(string userId, int rateLimit, double IntervalInSeconds)
        {
            using (UserDbContext ctx = new UserDbContext(new DbContextOptionsBuilder<UserDbContext>()
                     .UseInMemoryDatabase("TestDatabase")
                     .Options))
            {
                var nowutc = DateTime.UtcNow;
                var userlog = ctx.UserLogs.Where(x => x.UserId == userId
                && x.RequestTime >= nowutc.AddSeconds(-1 * IntervalInSeconds)).OrderBy(x => x.RequestTime).ToList();
                if (userlog.Count() >= rateLimit)
                {
                    return IntervalInSeconds - (nowutc - userlog.First().RequestTime).TotalSeconds;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
