using Microsoft.EntityFrameworkCore;

namespace AirTasker.Showcase.RateLimit.DataAccess
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(new DbContextOptionsBuilder<UserDbContext>()
                    .UseInMemoryDatabase("TestDatabase").Options)
        {

        }
        public virtual DbSet<UserLog> UserLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserLog>(entity =>
            {
                entity.HasKey(x => x.LogId);
            });
        }
    }
}
