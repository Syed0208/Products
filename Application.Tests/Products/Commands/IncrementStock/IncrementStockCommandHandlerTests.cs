using Domain.Entities;
using Domain.Repositories;
using Moq;
using FluentAssertions;
using Domain.Exceptions;

namespace Application.Products.Commands.IncrementStock.Tests
{
    public class IncrementStockCommandHandlerTests
    {
        private readonly Mock<IProductsRepository> _productsRepositoryMock;

        private readonly IncrementStockCommandHandler _handler;

        public IncrementStockCommandHandlerTests()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();

            _handler = new IncrementStockCommandHandler(
                _productsRepositoryMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldIncrementStock()
        {
            // Arrange
            int productId = 100000, quantity = 5;
            var existingProduct = new Product
            {
                Id = productId,
                Category = "Electronics",
                StockAvailable = 10,
                Description = "Description",
                ModifiedDate = DateTime.UtcNow.AddDays(-1)
            };
            var updatedProduct = new Product
            {
                Id = productId,
                Category = "Electronics",
                StockAvailable = 15,
                Description = "Description",
                ModifiedDate = DateTime.UtcNow
            };
            var command = new IncrementStockCommand(productId, quantity) { };

            _productsRepositoryMock.Setup(repo => repo.GetByIdAsync(productId))
                                  .ReturnsAsync(existingProduct);
            _productsRepositoryMock.Setup(repo => repo.UpdateStockAsync(It.IsAny<Product>()))
                                  .ReturnsAsync(updatedProduct);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _productsRepositoryMock.Verify(repo => repo.GetByIdAsync(productId), Times.Once);  // Ensure GetByIdAsync was called once
            _productsRepositoryMock.Verify(repo => repo.UpdateStockAsync(It.Is<Product>(p => p.StockAvailable == 15)), Times.Once);  // Ensure the stock was incremented by 5

            // Verifying the product was updated correctly
            updatedProduct.StockAvailable.Should().Be(15);  // Ensure the stock is incremented
            updatedProduct.ModifiedDate.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromSeconds(1));
        }

        [Fact]
        public async Task Handle_WithNonExistingProduct_ShouldThrowNotFoundException()
        {
            // Arrange
            int productId = 100000, quantity = 5;
            var request = new IncrementStockCommand(productId, quantity) { };

            _productsRepositoryMock.Setup(r => r.GetByIdAsync(productId))
                    .ReturnsAsync((Product?)null);

            // act
            Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);

            // assert
            await act.Should().ThrowAsync<NotFoundException>()
                    .WithMessage($"Product with id: {productId} doesn't exist");
        }
    }
}