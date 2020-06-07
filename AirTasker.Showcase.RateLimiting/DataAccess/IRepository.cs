namespace AirTasker.Showcase.RateLimiting.DataAccess
{
    public interface IRepository
    {
        double CalculateWaitingTime(string userId, int rateLimit, double IntervalInSeconds);
    }
}
