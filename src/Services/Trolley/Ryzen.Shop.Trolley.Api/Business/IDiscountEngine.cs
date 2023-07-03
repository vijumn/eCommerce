using Ryzen.Shop.Trolley.Api.Services;

namespace Ryzen.Shop.Trolley.Api.Business
{
    public interface IDiscountEngine
    {
        Task ApplyDiscount(List<Promotion> promotions, CustomerTrolley customerTrolley);
    }
}
