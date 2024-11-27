using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class ProductsRepository(ProductsDbContext dbContext) : IProductsRepository
    {
        public async Task<int> CreateAsync(Product entity)
        {
            dbContext.Products.Add(entity);
            await dbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            var products = await dbContext.Products.AsNoTracking().ToListAsync();
            return products;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var product = await dbContext.Products
           .FirstOrDefaultAsync(x => x.Id == id);

            return product;
        }

        public async Task<Product?> UpdateAsync(Product entity)
        {
            dbContext.Products.Update(entity);
            await dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(Product entity)
        {
            dbContext.Remove(entity);
            await dbContext.SaveChangesAsync();
        }

        public async Task<Product?> UpdateStockAsync(Product entity)
        {
            await dbContext.SaveChangesAsync();
            return entity;
        }


    }
}
