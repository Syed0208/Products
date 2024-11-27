using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Commands.DecrementStock
{
    public class DecrementStockCommandHandler(IProductsRepository productsRepository) : IRequestHandler<DecrementStockCommand>
    {
        public async Task Handle(DecrementStockCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException(nameof(Product), request.Id.ToString());
            var updatedQuantity = product.StockAvailable - request.Quantity;

            if (updatedQuantity < 0)
                throw new StockCannotBeNegativeException($"Cannot decrement stock. Current stock available: {product.StockAvailable}, decrement quantity: {request.Quantity}. Stock cannot be negative.");

            product.StockAvailable = updatedQuantity;
            product.ModifiedDate = DateTime.UtcNow;

            await productsRepository.UpdateStockAsync(product);
        }
    }
}
