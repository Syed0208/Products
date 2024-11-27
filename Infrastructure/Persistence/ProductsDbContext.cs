using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options)
    : base(options)
        {
        }
        internal DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasSequence<int>("ProductIdSequence")
            .StartsAt(100000)
            .IncrementsBy(1);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Id)
            .HasDefaultValueSql("NEXT VALUE FOR ProductIdSequence");

                entity.Property(p => p.Price)
            .HasPrecision(18, 2);

                entity.Property(p => p.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()")  // SQL function to get the current UTC date/time
             .ValueGeneratedOnAdd();

                entity.Property(p => p.ModifiedDate)
             .HasDefaultValueSql("GETUTCDATE()")  // SQL function to get the current UTC date/time
             .ValueGeneratedOnAdd();
            });

        }
    }
}
