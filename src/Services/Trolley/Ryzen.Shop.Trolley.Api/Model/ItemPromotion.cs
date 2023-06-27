namespace Ryzen.Shop.Trolley.Api.Model
{
    public class ItemPromotion : IPromotion
    {
        private readonly int _productId;
        private readonly decimal _amountOff;
        private readonly decimal _percentageOff;

        public ItemPromotion(dynamic data)
        {
            _productId = data.ProductId;
            _amountOff = data.AmountOff;
            _percentageOff = data.PercentageOff;
        }

        public  void Apply(CustomerTrolley trolley)
        {
            foreach (var product in trolley.Items)
            {
                if (product.ProductId == _productId)
                {
                    if (_amountOff > 0)
                    {
                        product.UnitPriceSale -= _amountOff;
                    }
                    else if (_percentageOff > 0)
                    {
                        decimal discount = product.UnitPrice * _percentageOff / 100;
                        product.UnitPriceSale -= discount;
                    }
                }
            }
        }
    }
}
