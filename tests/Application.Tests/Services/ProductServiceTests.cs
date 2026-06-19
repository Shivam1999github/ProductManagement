using Application.DTOs;
using Application.Interfaces;
using Application.Services;
using Domain.Entities;
using Moq;

namespace Application.Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _repositoryMock;
        private readonly ProductService _service;

        public ProductServiceTests()
        {
            _repositoryMock = new Mock<IProductRepository>();

            _service = new ProductService(_repositoryMock.Object);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnProducts()
        {
            // Arrange
            var products = new List<Product>
    {
        new Product
        {
            Id = 1,
            ProductName = "Laptop"
        }
    };

            _repositoryMock
                .Setup(x => x.GetAllAsync())
                .ReturnsAsync(products);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Single(result);

            Assert.Equal("Laptop", result.First().ProductName);
        }
        [Fact]
        public async Task GetByIdAsync_ShouldReturnProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Laptop"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            Assert.NotNull(result);

            Assert.Equal(1, result!.Id);
        }
        [Fact]
        public async Task CreateAsync_ShouldCreateProduct()
        {
            // Arrange
            var dto = new CreateProductDto
            {
                ProductName = "Laptop"
            };

            // Act
            await _service.CreateAsync(dto);

            // Assert
            _repositoryMock.Verify(
                x => x.AddAsync(It.IsAny<Product>()),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
        [Fact]
        public async Task UpdateAsync_ShouldUpdateProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                ProductName = "Old Name"
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            var dto = new UpdateProductDto
            {
                ProductName = "New Name"
            };

            // Act
            await _service.UpdateAsync(1, dto);

            // Assert
            Assert.Equal("New Name", product.ProductName);

            _repositoryMock.Verify(
                x => x.Update(product),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
        [Fact]
        public async Task DeleteAsync_ShouldDeleteProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1
            };

            _repositoryMock
                .Setup(x => x.GetByIdAsync(1))
                .ReturnsAsync(product);

            // Act
            await _service.DeleteAsync(1);

            // Assert
            _repositoryMock.Verify(
                x => x.Delete(product),
                Times.Once);

            _repositoryMock.Verify(
                x => x.SaveChangesAsync(),
                Times.Once);
        }
    }
}
