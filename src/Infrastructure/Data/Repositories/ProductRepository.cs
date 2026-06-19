using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Product
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Product
                 .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> GetProductWithItemsAsync(int id)
        {
            return await _context.Product
                .Include(x => x.Item)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<List<Product>> GetPagedAsync(int pageNumber, int pageSize)
        {
            return await _context.Product
                .AsNoTracking()
                .OrderBy(x => x.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Product.AddAsync(product);
        }

        public void Update(Product product)
        {
            _context.Product.Update(product);
        }

        public void Delete(Product product)
        {
            _context.Product.Remove(product);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}