using Application.Products.Dtos;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Queries.GetAllProducts
{
    public class GetProductByIdQueryHandler(IProductsRepository productsRepository,
        IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto>
    {
        public async Task<ProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id)
          ?? throw new NotFoundException(nameof(Product), request.Id.ToString());

            var productDto = mapper.Map<ProductDto>(product);
            return productDto;

        }
    }
}
