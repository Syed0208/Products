using Application.Products.Dtos;
using MediatR;

namespace Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
    {

    }
}
