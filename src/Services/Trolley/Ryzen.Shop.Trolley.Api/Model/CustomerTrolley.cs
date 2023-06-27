namespace Ryzen.Shop.Trolley.Api.Model;

public class CustomerTrolley
{
    public string CustomerId { get; set; }

    public List<TrolleyItem> Items { get; set; } = new();

    public decimal Subtotal { get; set; }
    public decimal CartDiscount { get; set; }
    public decimal Total { get; set; }

    public CustomerTrolley()
    {

    }

    public CustomerTrolley(string customerId)
    {
        CustomerId = customerId;
    }
}

