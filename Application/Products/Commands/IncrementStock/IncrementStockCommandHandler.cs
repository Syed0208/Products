using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Commands.IncrementStock
{
    public class IncrementStockCommandHandler(IProductsRepository productsRepository) : IRequestHandler<IncrementStockCommand>
    {
        public async Task Handle(IncrementStockCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id.ToString());

            product.StockAvailable += request.Quantity;
            product.ModifiedDate = DateTime.UtcNow;

            await productsRepository.UpdateStockAsync(product);
        }
    }
}
