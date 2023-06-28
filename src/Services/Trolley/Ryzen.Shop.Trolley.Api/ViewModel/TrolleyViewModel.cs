namespace Ryzen.Shop.Trolley.Api.ViewModel
{
    public class TrolleyViewModel
    {
            public List<TrolleyItemViewModel> Items { get; set; } = new();
    }

    public class TrolleyItemViewModel
    {
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; } //TODO:Remove this
        public int Quantity { get; set; }
    }
}
