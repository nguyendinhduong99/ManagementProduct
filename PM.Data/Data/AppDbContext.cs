using Microsoft.EntityFrameworkCore;
using PM.Data.Models;

namespace PM.Data.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set mqh N-N

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }


    }
}
