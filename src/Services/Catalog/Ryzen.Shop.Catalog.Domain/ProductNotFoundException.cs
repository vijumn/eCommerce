namespace Domain.Products;

public sealed class ProductNotFoundException : Exception
{
    public ProductNotFoundException(int id)
        : base($"The product with the ID = {id} was not found")
    {
    }
}
