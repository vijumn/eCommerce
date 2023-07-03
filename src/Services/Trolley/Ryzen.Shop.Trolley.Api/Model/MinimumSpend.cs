namespace Ryzen.Shop.Trolley.Api.Model
{
    public class MinimumSpend : IPromotion
    {
        private decimal _threshold;
        private decimal _amountOff;

        public MinimumSpend(dynamic data)
        {
            _threshold= data.MinimumSpend??0.0m;
            _amountOff= data.DiscountAmount??0.0m;
        }

        public int Order { get ; set; } = int.MaxValue;

        public  void Apply(Trolley trolley)
        {
            if (trolley.Subtotal >= _threshold)
            {
                trolley.CartDiscount += _amountOff;
            }
        }
    }
}
