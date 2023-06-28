namespace Ryzen.Shop.Trolley.Api.Model;

public class CustomerTrolley
{
    public string CustomerId { get; set; }

    public List<TrolleyItem> Items { get; set; } = new();

    public decimal Subtotal => Items.Select(x => (x.UnitPriceSale?? x.UnitPrice) * x.Quantity).Sum();
    public decimal CartDiscount { get; set; }
    public decimal Total => Subtotal - CartDiscount;

    public CustomerTrolley()
    {

    }

    public CustomerTrolley(string customerId)
    {
        CustomerId = customerId;
    }

    public int ItemsCount => Items.Select(x => x.Quantity).Sum();
}

