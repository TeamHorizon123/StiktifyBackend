using Microsoft.EntityFrameworkCore;

namespace GrpcServiceOrder.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Cart> Carts { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<Domain.Entities.OrderDetail> OrderDetails { get; set; }
        public DbSet<Domain.Entities.OrderTracking> OrderTrackings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Cart>().ToTable("Cart");
            modelBuilder.Entity<Domain.Entities.Order>().ToTable("Order");
            modelBuilder.Entity<Domain.Entities.OrderDetail>().ToTable("OrderDetail");
            modelBuilder.Entity<Domain.Entities.OrderTracking>().ToTable("OrderTracking");
        }
    }
}
