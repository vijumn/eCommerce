namespace Ryzen.Shop.Trolley.Api.Model
{
    public class SecondOneDiscountPercentage:IPromotion
    {
        public int _productId;
        public decimal _percentage;

        public SecondOneDiscountPercentage(dynamic data)
        {
            _productId = data.ProductId;
            _percentage = data.DiscountPercentage;
        }

        public int Order { get; set; } = 200;
        public void Apply(CustomerTrolley trolley)
        {
            foreach (var product in trolley.Items)
            {
                if (product.ProductId == _productId)
                {
                    if (product.Quantity % 2 == 0)
                    {
                        if(product.Quantity == 2)
                        {
                            var total =  product.UnitPrice + product.UnitPrice * (1 - _percentage / 100);
                            product.UnitPriceSale= total / product.Quantity;
                        }
                        else
                        {
                            var total = product.UnitPrice * (1 - _percentage / 100) * product.Quantity/2 + product.UnitPrice * product.Quantity/2;
                            product.UnitPriceSale = total / product.Quantity;
                        }
                    }
                    else
                    {
                        if (product.Quantity == 1)
                        {
                            product.UnitPriceSale = product.UnitPrice;
                        }
                        else
                        {
                            var total =  product.UnitPrice * (1 - _percentage / 100) + product.UnitPrice * (product.Quantity - 2);
                            product.UnitPriceSale = total / product.Quantity;

                        }

                    }
                }
            }
        }
    }
}
