using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GrpcServiceUser.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Shop> Shops { get; set; }
        public DbSet<Domain.Entities.ShopRating> ShopRatings { get; set; }
        public DbSet<Domain.Entities.ReceiveAddress> ReceiveAddresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Shop>()
                .ToTable("Shop")
                .HasMany(s => s.ShopRatings)
                .WithOne(sr => sr.Shop)
                .HasForeignKey(sr => sr.ShopId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.ShopRating>().ToTable("ShopRating");
            modelBuilder.Entity<Domain.Entities.ReceiveAddress>().ToTable("Address");
        }
    }
}
