using Domain.Entities;

namespace Domain.Repositories
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product?> GetByIdAsync(int id);
        Task<int> CreateAsync(Product entity);
        Task DeleteAsync(Product entity);

        Task<Product?> UpdateAsync(Product entity);

        Task<Product?> UpdateStockAsync(Product entity);

    }
}
