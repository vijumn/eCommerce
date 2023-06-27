namespace Ryzen.Shop.Trolley.Api.Model
{
    public class TrolleyItem
    {
        public string Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitPriceSale { get; set; }
        public int Quantity { get; set; }
    }
}
