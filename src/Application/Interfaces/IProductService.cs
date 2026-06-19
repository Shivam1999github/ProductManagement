using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponseDto>> GetAllAsync();

        Task<ProductResponseDto?> GetByIdAsync(int id);

        Task<int> CreateAsync(CreateProductDto dto);

        Task UpdateAsync(int id, UpdateProductDto dto);

        Task DeleteAsync(int id);
    }
}

