using Ryzen.Shop.Infrastructure;

namespace Ryzen.Shop.Trolley.Api.Exceptions;

public sealed class ProductNotFoundException : BaseException
{
    public ProductNotFoundException(int id)
        : base($"The product with the ID = {id} was not found")
    {
    }
}
