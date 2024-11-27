using MediatR;

namespace Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand(int id) : IRequest
    {
        public int Id { get; } = id;
    }
}
