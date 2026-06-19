using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<List<Product>> GetPagedAsync(int pageNumber, int pageSize);

        Task<Product?> GetByIdAsync(int id);

        Task<Product?> GetProductWithItemsAsync(int id);

        Task AddAsync(Product product);

        void Update(Product product);

        void Delete(Product product);

        Task SaveChangesAsync();
    }
}