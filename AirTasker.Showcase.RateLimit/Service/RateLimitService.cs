using AirTasker.Showcase.RateLimit.DataAccess;
using System;
using System.Linq;

namespace AirTasker.Showcase.RateLimit.Service
{
    public class RateLimitService : IRateLimitService
    {
        private readonly IRepository _repository;
        public RateLimitService(IRepository repository)
        {
            _repository = repository;
        }
        public double GetWaitingTime(string userId, int rateLimit, double IntervalInSeconds, DateTime date)
        {
            var userlog = _repository.GetUserLogs(userId, IntervalInSeconds, date);
            double waitingTime = 0;
            if (userlog.Count() >= rateLimit)
            {
                waitingTime = Math.Round(IntervalInSeconds - (date - userlog.First().RequestTime).TotalSeconds);
            }
            return waitingTime;
        }

        public void AddUserLog(string userId, DateTime nowutc)
        {
            _repository.AddUserLog(userId, nowutc);
        }
    }
}
