using AirTasker.Showcase.RateLimit.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirTasker.Showcase.RateLimit.Service
{
    public class RateLimitService : IRateLimitService
    {
        private readonly IRepository _repository;
        static Dictionary<string, double> userLogs = new Dictionary<string, double>();
        public RateLimitService(IRepository repository)
        {
            _repository = repository;
        }
        public double GetWaitingTime(string userId)
        {
            return userLogs.FirstOrDefault(x => x.Key == userId).Value;
        }

        public void SetRateLimit(string userId, int rateLimit, double IntervalInSeconds, DateTime date)
        {
            var userlog = _repository.GetUserLogs(userId, IntervalInSeconds, date);
            double waitingTime = 0;
            if (userlog.Count() >= rateLimit)
            {
                waitingTime = Math.Round(IntervalInSeconds - (date - userlog.First().RequestTime).TotalSeconds);
            }
            userLogs.Remove(userId);
            if (waitingTime > 0)
            {
                userLogs.Add(userId, waitingTime);
            }
        }

        public void AddUserLog(string userId, DateTime nowutc)
        {
            _repository.AddUserLog(userId, nowutc);
        }
    }
}
