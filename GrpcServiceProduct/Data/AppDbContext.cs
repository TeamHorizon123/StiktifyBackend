using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace GrpcServiceProduct.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Domain.Entities.Category> Categories { get; set; }
        public DbSet<Domain.Entities.CategorySize> CategorieSizes { get; set; }
        public DbSet<Domain.Entities.Product> Products { get; set; }
        public DbSet<Domain.Entities.ProductOption> ProductOptions { get; set; }
        public DbSet<Domain.Entities.ProductRating> ProductRatings { get; set; }
        public DbSet<Domain.Entities.ProductVarriant> ProductVarriants { get; set; }
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

            modelBuilder.Entity<Domain.Entities.CategorySize>()
                .ToTable("CategorySize")
                .HasOne<Domain.Entities.Category>()
                .WithMany(c => c.CategorySizes)
                .HasForeignKey(cs => cs.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Domain.Entities.CategorySize>()
                .HasOne<Domain.Entities.CategorySize>()
                .WithMany()
                .HasForeignKey(cs => cs.Id)
                .OnDelete(DeleteBehavior.SetNull);

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

            modelBuilder.Entity<Domain.Entities.ProductRating>()
                .ToTable("ProductRating")
                .HasOne<Domain.Entities.ProductRating>()
                .WithMany()
                .HasForeignKey(pr => pr.Id);

            modelBuilder.Entity<Domain.Entities.ProductVarriant>()
                .ToTable("ProductVarriant")
                .HasKey(pv => new { pv.ProductOptionId, pv.SizeId });

            modelBuilder.Entity<Domain.Entities.ProductVarriant>()
                .HasOne<Domain.Entities.ProductOption>()
                .WithMany(pv => pv.ProductVarriants)
                .HasForeignKey(pv => pv.ProductOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.ProductVarriant>()
                .HasOne<Domain.Entities.CategorySize>()
                .WithMany(pv => pv.ProductVarriants)
                .HasForeignKey(pv => pv.SizeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Domain.Entities.ProductItem>()
                .ToTable("ProductItem");
        }
    }
}
