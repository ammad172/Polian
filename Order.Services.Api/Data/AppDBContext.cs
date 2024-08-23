using Microsoft.EntityFrameworkCore;
using Order.Services.Api.Models;

namespace Order.Services.Api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<OrderHeader> orderHeaders { get; set; }

        public DbSet<OrderDetails> orderDetails { get; set; }
    }
}
