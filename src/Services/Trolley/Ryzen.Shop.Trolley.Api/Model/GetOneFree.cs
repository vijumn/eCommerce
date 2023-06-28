namespace Ryzen.Shop.Trolley.Api.Model
{
    public class GetOneFree:IPromotion
    {
        public int _productId;
        public int _quantity;
        
        public GetOneFree(dynamic data)
        {
            _productId = data.ProductId;
            _quantity = data.Quantity;
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
                        product.UnitPriceSale = product.UnitPrice / 2;
                    }
                    else
                    {
                        if(product.Quantity == 1)
                        {
                            product.UnitPriceSale = product.UnitPrice;
                        }
                        else
                        {
                            product.UnitPriceSale = (product.UnitPrice * ((product.Quantity - 1) / 2) + product.UnitPrice)/ product.Quantity;
                        }
                       
                    }
                }
            }
        }
    }
}
