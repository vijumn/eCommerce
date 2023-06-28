using Ryzen.Shop.Shared;

namespace Ryzen.Shop.Catalog.Domain
{
    public class PromotionType: Enumeration
    {

        public static PromotionType ItemDiscount = new (1,nameof(ItemDiscount));
        public static PromotionType MinimumSpend = new (2,nameof(MinimumSpend));
        public static PromotionType GetOneFree = new (3,nameof(GetOneFree));
        public static PromotionType SecondOneDiscountPercentage = new PromotionType(4, nameof(SecondOneDiscountPercentage));
        public static PromotionType TrollyDiscount = new PromotionType(5, nameof(TrollyDiscount));

              public PromotionType(int id, string name)
            : base(id, name)
        {
        }

        public static explicit operator int(PromotionType v)
        {
           return v.Id;
        }
    }
}
