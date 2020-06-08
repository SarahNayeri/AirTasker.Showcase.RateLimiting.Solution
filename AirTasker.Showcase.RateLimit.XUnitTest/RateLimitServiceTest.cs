using AirTasker.Showcase.RateLimit.DataAccess;
using AirTasker.Showcase.RateLimit.Service;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace AirTasker.Showcase.RateLimit.XUnitTest
{
    public class RateLimitServiceTest
    {
        private readonly Mock<IRepository> _repository;
        private readonly RateLimitService _rateLimitService;

        public RateLimitServiceTest()
        {
            _repository = new Mock<IRepository>();
            _rateLimitService = new RateLimitService(_repository.Object);
        }

        [Fact]
        public void GetWaitingTime_Test_When_GetUserLogs_Returns_No_Records()
        {
            //Setup
            _repository.Setup(x => x.GetUserLogsWithinInterval(It.IsAny<string>(), It.IsAny<double>(), It.IsAny<DateTime>()))
                .Returns(() => null);
            //Act
            var output = _rateLimitService.GetWaitingTime(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<DateTime>());
            //Assert
            Assert.Equal(0, output);
        }

        [Fact]
        public void GetWaitingTime_Test_When_User_Exceeds_RateLimit()
        {
            //Setup
            var userId = "1";
            var date = DateTime.UtcNow;
            int rateLimit = 2;
            double interval = 100;
            _repository.Setup(x =>
            x.GetUserLogsWithinInterval(userId, interval, date)).Returns(() => new List<UserLog>
            {
                new UserLog{UserId=userId, RequestTime = date.AddSeconds(-10)},
                new UserLog{UserId=userId, RequestTime = date.AddSeconds(-20)},
                new UserLog{UserId=userId, RequestTime = date.AddSeconds(-30)},
            });
            //Act
            var output = _rateLimitService.GetWaitingTime(userId, rateLimit, interval, date);
            //Assert
            Assert.Equal(70, output);
        }

        [Fact]
        public void GetWaitingTime_Test_When_User_Does_Not_Exceed_RateLimit()
        {
            //Setup
            var userId = "1";
            var date = DateTime.UtcNow;
            int rateLimit = 200;
            double interval = 100;
            _repository.Setup(x =>
            x.GetUserLogsWithinInterval(userId, interval, date)).Returns(() => new List<UserLog>
            {
                new UserLog{UserId=userId, RequestTime = date.AddSeconds(-10)},
                new UserLog{UserId=userId, RequestTime = date.AddSeconds(-20)},
                new UserLog{UserId=userId, RequestTime = date.AddSeconds(-30)},
            });
            //Act
            var output = _rateLimitService.GetWaitingTime(userId, rateLimit, interval, date);
            //Assert
            Assert.Equal(0, output);
        }
    }
}
