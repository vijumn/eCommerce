using MediatR;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Application.Query;
using Ryzen.Shop.Catalog.Application.Data;

namespace Ryzen.Shop.Catalog.Application.Query;

internal sealed class GetAllroductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductDetailResponse[]>
{
    private readonly ICatalogContext _context;

    public GetAllroductsQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductDetailResponse[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
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
            .ToArrayAsync(cancellationToken);


        return product;
    }
}
