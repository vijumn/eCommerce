﻿using System.Reflection;

namespace Ryzen.Shop.Trolley.Api.Business
{
    public class DiscountFactory : IDiscountFactory
    {
        private static Dictionary<string, Type> _promotionTypes;

        public DiscountFactory()
        {
            _promotionTypes = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IPromotion).IsAssignableFrom(t) && !t.IsInterface)
                .ToDictionary(t => t.Name, t => t, StringComparer.OrdinalIgnoreCase);
        }

        public IPromotion CreatePromotion(string promotionType, dynamic data)
        {
            if (_promotionTypes.TryGetValue(promotionType, out var promotionClass))
            {
                return Activator.CreateInstance(promotionClass, data) as IPromotion;
            }

            throw new ArgumentException($"Invalid promotion type: {promotionType}");
        }
    }
}
