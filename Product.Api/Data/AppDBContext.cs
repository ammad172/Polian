using Microsoft.EntityFrameworkCore;
using Product.Api.Model;
namespace Product.Api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        public DbSet<Products> products { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed initial data
            modelBuilder.Entity<Products>().HasData(
                new Products
                {
                    ProductId = 1,
                    Name = "Product 1",
                    Description = "Description for Product 1",
                    CategoryName = "Category 1",
                    ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Funsplash.com%2Fs%2Fphotos%2Fproducts&psig=AOvVaw3CKSM2sxX786LUDZsD_TgV&ust=1705242395279000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCICwl4nJ2oMDFQAAAAAdAAAAABAJ",
                    Price = 19.99m,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                },
                new Products
                {
                    ProductId = 2,
                    Name = "Product 2",
                    Description = "Description for Product 2",
                    CategoryName = "Category 2",
                    ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.pexels.com%2Fsearch%2Fproduct%2F&psig=AOvVaw3CKSM2sxX786LUDZsD_TgV&ust=1705242395279000&source=images&cd=vfe&ved=0CBMQjRxqFwoTCICwl4nJ2oMDFQAAAAAdAAAAABAE",
                    Price = 29.99m,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = null
                }
            // Add more seed data as needed
            );
        }
    }
}
