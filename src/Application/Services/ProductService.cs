using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();

            return products.Select(x => new ProductResponseDto
            {
                Id = x.Id,
                ProductName = x.ProductName
            });
        }
        public async Task<IEnumerable<ProductResponseDto>> GetProducts(PaginationRequest request)
        {
            var products = await _repository.GetPagedAsync(request.PageNumber,request.PageSize);

            return products.Select(x => new ProductResponseDto
            {
                Id = x.Id,
                ProductName = x.ProductName
            });
        }

        public async Task<ProductResponseDto?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                return null;

            return new ProductResponseDto
            {
                Id = product.Id,
                ProductName = product.ProductName
            };
        }

        public async Task<int> CreateAsync(CreateProductDto dto)
        {
            var product = new Product
            {
                ProductName = dto.ProductName,
                CreatedBy = "System", // JWT implement hone ke baad current user use kar lena
                CreatedOn = DateTime.UtcNow
            };

            await _repository.AddAsync(product);

            await _repository.SaveChangesAsync();

            return product.Id;
        }

        public async Task UpdateAsync(int id, UpdateProductDto dto)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with Id {id} not found.");

            product.ProductName = dto.ProductName;
            product.ModifiedBy = "System";
            product.ModifiedOn = DateTime.UtcNow;

            _repository.Update(product);

            await _repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);

            if (product == null)
                throw new KeyNotFoundException($"Product with Id {id} not found.");

            _repository.Delete(product);

            await _repository.SaveChangesAsync();
        }
    }
}