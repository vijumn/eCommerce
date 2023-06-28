namespace Ryzen.Shop.Trolley.Api.Model
{
    public interface IPromotion { 
        void Apply(CustomerTrolley trolley);
         int Order { get; set; }
    }
}
