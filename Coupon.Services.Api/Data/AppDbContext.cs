using Coupon.Services.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace Coupon.Services.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : base(dbContextOptions) { 
        }

        public DbSet<CouponModel> Coupons { get; set; }


    }


}
