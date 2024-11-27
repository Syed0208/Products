using Application.Products.Dtos;
using MediatR;

namespace Application.Products.Queries.GetAllProducts
{
    public class GetProductByIdQuery(int id) : IRequest<ProductDto>
    {
        public int Id { get; } = id;
    }
}
