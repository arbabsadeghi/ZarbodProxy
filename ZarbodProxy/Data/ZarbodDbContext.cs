using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ZarbodProxy
{
    public class ZarbodDbContext : DbContext
    {
        public DbSet<ApiLog> ApiLogs { get; set; }
        public ZarbodDbContext(DbContextOptions<ZarbodDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
