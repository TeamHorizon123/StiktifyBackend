using Microsoft.EntityFrameworkCore;

namespace GrpcServicePurchase.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Payment> Payments { get; set; }
        public DbSet<Domain.Entities.PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Domain.Entities.PaymentRefund> PaymentRefunds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Payment>().ToTable("Payment");
            modelBuilder.Entity<Domain.Entities.PaymentMethod>().ToTable("PaymentMethod");
            modelBuilder.Entity<Domain.Entities.PaymentRefund>().ToTable("PaymentRefund");
        }
    }
}
