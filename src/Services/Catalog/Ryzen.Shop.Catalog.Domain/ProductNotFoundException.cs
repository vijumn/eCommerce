namespace Ryzen.Shop.Catalog.Domain;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int id)
        : base($"The product with the ID = {id} was not found")
    {
    }
}
