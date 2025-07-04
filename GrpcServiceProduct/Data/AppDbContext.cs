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
        public DbSet<Domain.Entities.OptionSize> OptionSizes { get; set; }
        public DbSet<Domain.Entities.OptionSizeColor> OptionColors { get; set; }
        public DbSet<Domain.Entities.ProductItem> ProductItems { get; set; }

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
                .HasMany(c => c.Categories)
                .WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>
                (
                        "CategoryItem",
                        j => j.HasOne<Domain.Entities.Category>()
                               .WithMany()
                               .HasForeignKey("CategoryId")
                               .OnDelete(DeleteBehavior.Cascade),
                         j => j.HasOne<Domain.Entities.Product>()
                               .WithMany()
                               .HasForeignKey("ProductId")
                               .OnDelete(DeleteBehavior.Cascade)
                );

            modelBuilder.Entity<Domain.Entities.Product>()
                .ToTable("Product")
                .HasOne<Domain.Entities.Product>()
                .WithMany()
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.ProductOption>()
                .ToTable("ProductOption")
                .HasOne<Domain.Entities.ProductOption>()
                .WithMany()
                .HasForeignKey(po => po.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.ProductRating>().ToTable("ProductRating");
            modelBuilder.Entity<Domain.Entities.OptionSize>()
                .ToTable("OptionSize")
                .HasOne<Domain.Entities.OptionSize>()
                .WithMany()
                .HasForeignKey(os => os.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.OptionSizeColor>()
                .ToTable("OptionSizeColor")
                .HasKey(sc => new { sc.OptionId, sc.OptionSize });

            modelBuilder.Entity<Domain.Entities.ProductItem>()
                .ToTable("ProductItem");
        }
    }
}
