using Ryzen.Shop.Trolley.Api.Services;

namespace Ryzen.Shop.Trolley.Api.Business
{
    public interface IDiscountEngine
    {
        Task<Model.Trolley> ApplyDiscount(List<Promotion> promotions, Model.Trolley customerTrolley);
    }
}
