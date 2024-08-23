using Microsoft.EntityFrameworkCore;
using ShoppingCart.Services.Api.Models;
namespace ShoppingCart.Api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<ShoppingCartHeader> shoppingCartHeaders { get; set; }

        public DbSet<ShoppingCartDetail> shoppingCartDetails { get; set; }
    }
}
