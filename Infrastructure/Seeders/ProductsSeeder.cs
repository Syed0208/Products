using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeders
{
    internal class ProductsSeeder(ProductsDbContext dbContext) : IProductsSeeder
    {
        public async Task Seed()
        {
            if (dbContext.Database.GetPendingMigrations().Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            if (await dbContext.Database.CanConnectAsync())
            {
                if (!dbContext.Products.Any())
                {
                    var products = GetProducts();
                    dbContext.Products.AddRange(products);
                    await dbContext.SaveChangesAsync();
                }
            }
        }

        private IEnumerable<Product> GetProducts()
        {

            List<Product> products = [
                new()
            {
                Name = "Product 1",
                Category = "Electronics",
                Description =
                    "A description about product 1",
                Price = 1000,
                StockAvailable = 10
            },
            new ()
            {
                Name = "Product 2",
                Category = "Mobiles",
                Description =
                    "Product 2 has good camera and processor.",
                Price = 2500,
                StockAvailable = 5
            }
            ];

            return products;
        }

    }
}
