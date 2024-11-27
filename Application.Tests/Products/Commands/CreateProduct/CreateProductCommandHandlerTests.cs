using AutoMapper;
using Microsoft.Extensions.Logging;
using Domain.Entities;
using Domain.Repositories;
using Moq;
using FluentAssertions;

namespace Application.Products.Commands.CreateProduct.Tests
{
    public class CreateProductCommandHandlerTests
    {
        [Fact()]
        public async Task Handle_ForValidCommand_ReturnsCreatedProductId()
        {
            // arrange
            var loggerMock = new Mock<ILogger<CreateProductCommandHandler>>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateProductCommand();
            var product = new Product();

            mapperMock.Setup(m => m.Map<Product>(command)).Returns(product);

            var productRepositoryMock = new Mock<IProductsRepository>();
            productRepositoryMock
                .Setup(repo => repo.CreateAsync(It.IsAny<Product>()))
                .ReturnsAsync(100000);


            var commandHandler = new CreateProductCommandHandler(productRepositoryMock.Object,
                mapperMock.Object);

            // act
            var result = await commandHandler.Handle(command, CancellationToken.None);

            // assert
            result.Should().Be(100000);
            productRepositoryMock.Verify(r => r.CreateAsync(product), Times.Once);
        }
    }
}