using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IProductsRepository productsRepository,
        IMapper mapper) : IRequestHandler<CreateProductCommand, int>
    {
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            int id = await productsRepository.CreateAsync(product);
            return id;
        }
    }
}
