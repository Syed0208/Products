using MediatR;

namespace Application.Products.Commands.DecrementStock
{
    public class DecrementStockCommand(int id, int quantity) : IRequest
    {
        public int Id { get; } = id;
        public int Quantity { get; } = quantity;
    }
}
