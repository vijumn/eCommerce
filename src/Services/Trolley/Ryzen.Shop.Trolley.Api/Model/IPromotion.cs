namespace Ryzen.Shop.Trolley.Api.Model
{
    public interface IPromotion { 
        void Apply(Trolley trolley);
         int Order { get; set; }
    }
}
