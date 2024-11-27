using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductsRepository productsRepository,
        IMapper mapper) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id);
            if (product is null)
                throw new NotFoundException(nameof(Product), request.Id.ToString());

            mapper.Map(request, product);
            product.ModifiedDate = DateTime.UtcNow;
            await productsRepository.UpdateAsync(product);
        }
    }
}
