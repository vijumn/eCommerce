namespace Ryzen.Shop.Trolley.Api.Business
{
    public interface IDiscountFactory
    {
        IPromotion CreatePromotion(string promotionType, dynamic data);
    }
}