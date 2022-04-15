using ManagementProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementProductAPI.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //set mqh N-N
            modelBuilder.Entity<OrderDetails>()
                .HasOne(p => p.Products)
                .WithMany(po => po.OrderDetails)
                .HasForeignKey(pi => pi.Id);

            modelBuilder.Entity<OrderDetails>()
                .HasOne(o => o.Orders)
                .WithMany(po => po.OrderDetails)
                .HasForeignKey(oi => oi.Id);
        }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Payments> Payments { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<Shippers> Shippers { get; set; }
        public DbSet<Suppliers> Suppliers { get; set; }
    
    }
}
