using Microsoft.EntityFrameworkCore;

namespace GrpcServiceProduct.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Category> Categories { get; set; }
        public DbSet<Domain.Entities.Product> Products { get; set; }
        public DbSet<Domain.Entities.ProductOption> ProductOptions { get; set; }
        public DbSet<Domain.Entities.ProductRating> ProductRatings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Category>()
                .ToTable("Category")
                .HasOne(c => c.Parent)
                .WithMany(c => c.Children)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Domain.Entities.Category>()
                .HasMany(c => c.Products)
                .WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>
                (
                        "CategoryItem",
                         j => j.HasOne<Domain.Entities.Product>()
                               .WithMany()
                               .HasForeignKey("ProductId")
                               .OnDelete(DeleteBehavior.Cascade),
                         j => j.HasOne<Domain.Entities.Category>()
                               .WithMany()
                               .HasForeignKey("CategoryId")
                               .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<Domain.Entities.Product>()
                .ToTable("Product")
                .HasOne<Domain.Entities.Product>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.ProductOption>().ToTable("ProductOption");
            modelBuilder.Entity<Domain.Entities.ProductRating>().ToTable("ProductRating");
        }
    }
}
