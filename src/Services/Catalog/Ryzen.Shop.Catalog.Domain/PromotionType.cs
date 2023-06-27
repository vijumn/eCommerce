using Ryzen.Shop.Shared;

namespace Ryzen.Shop.Catalog.Domain
{
    public class PromotionType: Enumeration
    {

        public static PromotionType DiscountAmount = new (1,nameof(DiscountAmount));
        public static PromotionType DiscountPercentage = new (2,nameof(DiscountPercentage));
        public static PromotionType MinimumSpend = new (3,nameof(MinimumSpend));
        public static PromotionType GetOneFree = new (4,nameof(GetOneFree));
        public static PromotionType SecondOneDiscountPercentage = new PromotionType(5, nameof(SecondOneDiscountPercentage));
        public static PromotionType TrollyDiscountPercentage = new PromotionType(6, nameof(TrollyDiscountPercentage));

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
