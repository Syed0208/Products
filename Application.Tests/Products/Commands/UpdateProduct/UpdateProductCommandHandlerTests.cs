using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using FluentAssertions;
using Moq;

namespace Application.Products.Commands.UpdateProduct.Tests
{
    public class UpdateProductCommandHandlerTests
    {
        private readonly Mock<IProductsRepository> _productsRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        private readonly UpdateProductCommandHandler _handler;

        public UpdateProductCommandHandlerTests()
        {
            _productsRepositoryMock = new Mock<IProductsRepository>();
            _mapperMock = new Mock<IMapper>();

            _handler = new UpdateProductCommandHandler(
                _productsRepositoryMock.Object,
                _mapperMock.Object);
        }

        [Fact()]
        public async Task Handle_WithValidRequest_ShouldUpdateProducts()
        {
            // arrange
            var productId = 100000;
            var existingProduct = new Product
            {
                Id = productId,
                Name = "Old Name",
                Category = "Mobiles",
                Description = "Old Description",
                Price = 100,
                ModifiedDate = DateTime.UtcNow.AddDays(-1)
            };
            var command = new UpdateProductCommand()
            {
                Id = productId,
                Name = "Test Update",
                Category = "Appliances",
                Description = "Test update description",
                Price = 243
            };

            var updatedProduct = new Product()
            {
                Id = productId,
                Name = "Test",
                Category = "Mobiles",
                Description = "Test",
                Price = 100,
                ModifiedDate = DateTime.UtcNow
            };

            _mapperMock.Setup(m => m.Map(command, existingProduct)).Returns(updatedProduct);

            _productsRepositoryMock.Setup(r => r.GetByIdAsync(productId))
                .ReturnsAsync(existingProduct);
            _productsRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(updatedProduct);

            // act
            await _handler.Handle(command, CancellationToken.None);

            // assert

            _productsRepositoryMock.Verify(r => r.UpdateAsync(It.IsAny<Product>()), Times.Once);
            _mapperMock.Verify(m => m.Map(command, existingProduct), Times.Once);
        }

        [Fact]
        public async Task Handle_WithNonExistingProduct_ShouldThrowNotFoundException()
        {
            // Arrange
            var productId = 2;
            var request = new UpdateProductCommand
            {
                Id = productId
            };

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