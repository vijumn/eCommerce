using MediatR;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Application.Data;
using Ryzen.Shop.Catalog.Domain;

namespace Ryzen.Shop.Catalog.Application.Query;

internal sealed class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductDetailResponse>
{
    private readonly ICatalogContext _context;

    public GetProductQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductDetailResponse> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Where(p => p.ProductID==request.ProductId)
            .Select(p => new ProductDetailResponse(
                p.ProductID,
                p.Name,
                p.Description,
                p.Price,
                p.PromotionID,
                p.ProductPromotion.Type,
                p.ProductPromotion.DiscountAmount,
                p.ProductPromotion.DiscountPercentage,
                p.ProductPromotion.MinimumSpendAmount,
                p.ProductPromotion.GetOneFree,
                p.ProductPromotion.SecondOneDiscountPercentage
                ))
            .FirstOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw new ProductNotFoundException(request.ProductId);
        }

        return product;
    }
}
