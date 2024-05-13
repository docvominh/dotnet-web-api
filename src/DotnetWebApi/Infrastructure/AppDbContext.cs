using Infrastructure.Entity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<ProductEntity> Products { get; set; }

    public DbSet<TagEntity> Tags { get; set; }

    public DbSet<OrderEntity> Orders { get; set; }

    public DbSet<OrderProductEntity> OrderProducts { get; set; }

    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer();
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductEntity>(
            e =>
            {
                e.ToTable("Products");
                e.HasKey(x => new { x.Id }).HasName("PK_ProductId");
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Url).HasMaxLength(255);
                e.Property(x => x.Name).HasMaxLength(255);
                e.Property(x => x.Price).HasPrecision(18, 2);
                e.Property(x => x.CreatedDate).HasColumnType("datetimeoffset");
                e.Property(x => x.Description).HasColumnType("text").HasMaxLength(255);

                e.HasOne(product => product.Details)
                    .WithOne()
                    .HasForeignKey<ProductDetailEntity>(productDetail => productDetail.ProductId)
                    .IsRequired(false);


                // Product - Tag join table

                e.HasMany(t => t.Tags)
                    .WithMany(p => p.Products)
                    .UsingEntity(
                        "ProductTags",
                        l => l.HasOne(typeof(TagEntity))
                            .WithMany()
                            .HasForeignKey("TagId")
                            .HasConstraintName("FK_Tag_TagId")
                            .HasPrincipalKey(nameof(TagEntity.Id)),
                        r => r.HasOne(typeof(ProductEntity))
                            .WithMany()
                            .HasForeignKey("ProductId")
                            .HasConstraintName("FK_Product_ProductId")
                            .HasPrincipalKey(nameof(ProductEntity.Id)),
                        j => j.HasKey("ProductId", "TagId").HasName("PK_Product_Tag")
                    );
            }
        );

        modelBuilder.Entity<ProductDetailEntity>(
            e =>
            {
                e.ToTable("ProductDetails");
                e.HasKey(x => new { x.ProductId }).HasName("PK_ProductDetail");
                e.Property(x => x.Weight).HasPrecision(10, 2);
            }
        );

        modelBuilder.Entity<TagEntity>(
            e =>
            {
                e.ToTable("Tags");
                e.HasKey(x => new { x.Id }).HasName("PK_Tag");
                e.Property(x => x.Id).ValueGeneratedOnAdd();
                e.Property(x => x.Name).HasMaxLength(255);
                e.Property(x => x.Description).HasMaxLength(255);
            }
        );


        modelBuilder.Entity<OrderEntity>(
            e =>
            {
                e.ToTable("Orders");
                e.HasKey(x => new { x.Id }).HasName("PK_Order");
                e.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn(1000);
                e.Property(x => x.Total).HasPrecision(18, 2);
                e.Property(x => x.State).HasMaxLength(50);
                e.Property(x => x.ShippingAddress).HasMaxLength(255);
                e.Property(x => x.StripePaymentIntentId).HasMaxLength(50);
                e.Property(x => x.NabTransactionId).HasMaxLength(255);
                e.Property(x => x.CreatedDate).HasColumnType("datetimeoffset");

                e.HasOne(order => order.Customer)
                    .WithOne()
                    .HasForeignKey<OrderEntity>(nameof(OrderEntity.CustomerId))
                    .HasConstraintName("FK_Order_User_Username");
            }
        );

        modelBuilder.Entity<OrderProductEntity>(
            e =>
            {
                e.ToTable("OrderProducts");
                e.Property(x => x.OrderId);
                e.Property(x => x.ProductId);
                e.HasKey(x => new { x.OrderId, x.ProductId }).HasName("PK_OrderProduct");
                e.Property(x => x.Quantity);

                e.HasOne(orderProduct => orderProduct.Order)
                    .WithMany(order => order.OrderProducts)
                    .HasForeignKey(orderProduct => orderProduct.OrderId)
                    .HasConstraintName("FK_Order_Product_OrderId");

                e.HasOne(orderProduct => orderProduct.Product)
                    .WithMany(product => product.OrderProducts)
                    .HasForeignKey(orderProduct => orderProduct.ProductId)
                    .HasConstraintName("FK_Order_Product_ProductId");
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
