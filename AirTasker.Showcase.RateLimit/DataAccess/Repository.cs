using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirTasker.Showcase.RateLimit.DataAccess
{
    public class Repository : IRepository
    {
        protected DbContextOptions<UserDbContext> ContextOptions { get; }


        public Repository()
        {
            ContextOptions = new DbContextOptionsBuilder<UserDbContext>()
                     .UseInMemoryDatabase("TestDatabase")
                     .Options;
            Seed();
        }

        private void Seed()
        {
            using (var context = new UserDbContext(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                var userList = new List<UserLog>();
                for (var i = 1; i < 110; i++)
                {
                    userList.Add(new UserLog
                    {
                        LogId = Guid.NewGuid(),
                        UserId = TempConstant.DummyUser,
                        RequestTime = DateTime.UtcNow.AddSeconds(-10 * i)
                    });
                }
                context.AddRange(userList);
                context.SaveChanges();
            }
        }

        public void AddUserLog(string userId, DateTime nowutc)
        {
            using (var context = new UserDbContext(ContextOptions))
            {
                context.UserLogs.Add(new UserLog { LogId = Guid.NewGuid(), UserId = userId, RequestTime = nowutc });
                context.SaveChanges();
            }
        }

        public List<UserLog> GetUserLogs(string userId, double IntervalInSeconds, DateTime date)
        {
            using (var context = new UserDbContext(ContextOptions))
            {
                return context.UserLogs.Where(x => x.UserId == userId
            && x.RequestTime >= date.AddSeconds(-1 * IntervalInSeconds)).OrderBy(x => x.RequestTime).ToList();
            }
        }
    }
}
