using Application.Products.Dtos;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(IProductsRepository productsRepository,
        IMapper mapper) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productsRepository.GetAllAsync();
            var productsDtosList = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDtosList;

        }
    }
}
