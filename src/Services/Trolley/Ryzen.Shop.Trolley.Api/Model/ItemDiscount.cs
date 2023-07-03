namespace Ryzen.Shop.Trolley.Api.Model
{
    public class ItemDiscount : IPromotion
    {
        private readonly int _productId;
        private readonly decimal _amountOff;
        private readonly decimal _percentageOff;
        private readonly decimal _amount;

        public ItemDiscount(dynamic data)
        {
            _productId = data.ProductId;
            _amountOff = data.DiscountAmount?? 0.0m;
            _percentageOff = data.DiscountPercentage?? 0.0m; 
        }

        public int Order { get; set; } = 100;

        public  void Apply(Trolley trolley)
        {
            foreach (var product in trolley.Items)
            {
                if (product.ProductId == _productId)
                {
                    if (_amountOff > 0)
                    {
                        product.UnitPriceSale = product.UnitPrice- _amountOff;
                    }
                    else if (_percentageOff > 0)
                    {
                        decimal discount = product.UnitPrice * _percentageOff / 100;
                        product.UnitPriceSale = product.UnitPrice - discount;
                    }
                }
            }
        }
    }
}
