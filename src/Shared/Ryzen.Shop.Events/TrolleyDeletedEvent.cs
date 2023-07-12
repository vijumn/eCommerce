namespace Ryzen.Shop.Events
{
    public class TrolleyDeletedEvent
    {
        public string CustomerId { get; set; }  
        public decimal Total { get; set; }  
        public decimal TotalDiscount { get; set; }
        public List<TrolleyItem> Items { get; set; }

        public TrolleyDeletedEvent()
        {
                Items= new List<TrolleyItem>();
        }
        public class TrolleyItem
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }   
        }
    }
}
