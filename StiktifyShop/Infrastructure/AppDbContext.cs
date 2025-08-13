using Microsoft.EntityFrameworkCore;
using StiktifyShop.Domain.Entity;

namespace StiktifyShop.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        #region DbSets
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderTracking> OrderTrackings { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentRefund> PaymentRefunds { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductVariant> ProductVariants { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<ProductRating> ProductRatings { get; set; }
        public DbSet<ProductOption> ProductOptions { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<ShopRating> ShopRatings { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>().ToTable("Carts");
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Variant)
                .WithMany(pv => pv.Carts)
                .HasForeignKey(c => c.VariantId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Option)
                .WithMany(po => po.Carts)
                .HasForeignKey(c => c.OptionId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            #region model builder category
            modelBuilder.Entity<Category>().ToTable("Categories");
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Children)
                .WithOne(c => c.Parent)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
            modelBuilder.Entity<Order>().ToTable("Orders");
            //modelBuilder.Entity<Order>()
            //    .HasMany(o => o.OrderTrackings)
            //    .WithOne(ot => ot.Order)
            //    .HasForeignKey(ot => ot.OrderId)
            //    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Payment)
                .WithOne(p => p.Order)
                .HasForeignKey<Payment>(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<OrderDetail>().ToTable("OrderDetails");
            //modelBuilder.Entity<OrderDetail>()
            //    .HasOne(od => od.Order)
            //    .WithOne(o => o.OrderDetail)
            //    .HasForeignKey<OrderDetail>(od => od.Id)
            //    .OnDelete(DeleteBehavior.Cascade);
            //modelBuilder.Entity<OrderTracking>().ToTable("OrderTrackings");
            //modelBuilder.Entity<OrderTracking>()
            //    .HasOne(ot => ot.Order)
            //    .WithMany(o => o.OrderTrackings)
            //    .HasForeignKey(ot => ot.OrderId)
            //    .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems");
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.ProductVariant)
                .WithMany(pv => pv.OrderItems)
                .HasForeignKey(oi => oi.ProductVariantId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(c => c.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Payment>().ToTable("Payments");
            modelBuilder.Entity<PaymentMethod>().ToTable("PaymentMethods");
            modelBuilder.Entity<PaymentMethod>()
                .HasMany(pm => pm.Payments)
                .WithOne(p => p.PaymentMethod)
                .HasForeignKey(p => p.MethodId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<PaymentRefund>().ToTable("PaymentRefunds");

            #region model builder product
            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductOptions)
                .WithOne(po => po.Product)
                .HasForeignKey(po => po.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductItems)
                .WithOne(pi => pi.Product)
                .HasForeignKey(pi => pi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Product>()
                .HasMany(p => p.ProductRatings)
                .WithOne(pr => pr.Product)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            modelBuilder.Entity<ProductItem>().ToTable("ProductItems");

            modelBuilder.Entity<ProductOption>().ToTable("ProductOptions");
            modelBuilder.Entity<ProductOption>()
                .HasMany(po => po.ProductVariants)
                .WithOne(pv => pv.ProductOption)
                .HasForeignKey(pv => pv.ProductOptionId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductRating>().ToTable("ProductRatings");
            modelBuilder.Entity<ProductRating>()
                .HasOne(pr => pr.Product)
                .WithMany(p => p.ProductRatings)
                .HasForeignKey(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ProductRating>()
                .HasOne(pr => pr.Order)
                .WithOne(o => o.Rating)
                .HasForeignKey<ProductRating>(pr => pr.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSize>().ToTable("ProductSizes");

            modelBuilder.Entity<ProductSize>()
                .HasOne(ps => ps.Category)
                .WithMany(c => c.ProductSizes)
                .HasForeignKey(ps => ps.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductSize>()
                .HasMany(ps => ps.Variants)
                .WithOne(v => v.ProductSize)
                .HasForeignKey(v => v.SizeId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<ProductVariant>().ToTable("ProductVariants");

            #region model builder shop
            modelBuilder.Entity<Shop>().ToTable("Shops");
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.Products)
                .WithOne(p => p.Shop)
                .HasForeignKey(p => p.ShopId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Shop>()
                .HasMany(s => s.ShopRatings)
                .WithOne(sr => sr.Shop)
                .HasForeignKey(sr => sr.ShopId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<ShopRating>().ToTable("ShopRatings");
            #endregion

            modelBuilder.Entity<UserAddress>().ToTable("UserAddresses");
            modelBuilder.Entity<UserAddress>()
                .HasMany(ua => ua.Orders)
                .WithOne(o => o.Address)
                .HasForeignKey(o => o.AddressId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
