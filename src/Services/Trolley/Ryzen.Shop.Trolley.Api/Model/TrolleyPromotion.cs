namespace Ryzen.Shop.Trolley.Api.Model
{
    public class TrolleyPromotion : IPromotion
    {
        private decimal _threshold;
        private decimal _amountOff;

        public TrolleyPromotion(dynamic data)
        {
            _threshold= data.MinimumSpendAmount;
            _amountOff= data.MinimumSpendDiscountAmount;
        }
        public  void Apply(CustomerTrolley trolley)
        {
            if (trolley.Subtotal >= _threshold)
            {
                trolley.Total -= _amountOff;
                trolley.CartDiscount += _amountOff;
            }
        }
    }
}
