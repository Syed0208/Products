using MediatR;
namespace Application.Products.Commands.IncrementStock
{
    public class IncrementStockCommand(int id, int quantity) : IRequest
    {
        public int Id { get; } = id;
        public int Quantity { get; } = quantity;
    }
}
