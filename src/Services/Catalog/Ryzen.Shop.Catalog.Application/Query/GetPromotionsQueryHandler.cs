using MediatR;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Application.Data;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Application.Query;

internal sealed class GetPromotionsQueryHander : IRequestHandler<GetPromotionsQuery, PromotionResponse[]>
{
    private readonly ICatalogContext _context;

    public GetPromotionsQueryHander(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<PromotionResponse[]> Handle(GetPromotionsQuery request, CancellationToken cancellationToken)
    {
        var productPromotions = await _context
            .Products
            .Where(p => request.ProductIds.Contains(p.ProductID) && p.PromotionID !=null )
            .Select(p => new PromotionResponse(
                p.PromotionID,
                p.Price,
                p.ProductID,
                p.ProductPromotion.Type,
                p.ProductPromotion.DiscountAmount,
                p.ProductPromotion.DiscountPercentage,
                p.ProductPromotion.MinimumSpendAmount,
                p.ProductPromotion.GetOneFree,
                p.ProductPromotion.SecondOneDiscountPercentage
                ))
            .ToArrayAsync(cancellationToken);

        var trolleyPromotions = await _context.Promotions
        .Where(p => p.Type == PromotionType.MinimumSpend)
        .Select(p=> new PromotionResponse(
                       p.PromotionID,
                        0.0m,
                        null,
                        p.Type,
                        p.DiscountAmount,
                        p.DiscountPercentage,
                        p.MinimumSpendAmount,
                        p.GetOneFree,
                        p.SecondOneDiscountPercentage
                        ))
        .ToListAsync(cancellationToken);

        var allPromotions = productPromotions.Union(trolleyPromotions).ToList();

        return allPromotions.ToArray();
    }
}
