namespace Ryzen.Shop.Trolley.Api.ViewModel
{
    public class TrolleyViewModel
    {
            public List<TrolleyItemViewModel> Items { get; set; } = new();
    }

    public class TrolleyItemViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
