namespace Domain.Exceptions
{
    public class StockCannotBeNegativeException(string message)
        : Exception(message)
    {
    }
}
