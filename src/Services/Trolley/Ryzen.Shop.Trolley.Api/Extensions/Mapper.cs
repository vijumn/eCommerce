using Ryzen.Shop.Events;

namespace Ryzen.Shop.Trolley.Api.Extensions
{
    public static class Mapper
    {
        public static TrolleyDeletedEvent MapToTrolleyDeletedEvent(this Model.Trolley trolley)
        {
           var @event =  new TrolleyDeletedEvent
            {
                CustomerId = trolley.CustomerId
            };

            foreach (var item in trolley.Items)
            {
                @event.Items.Add(new TrolleyDeletedEvent.TrolleyItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
            }   

            return @event;
        }
    }
}
