using Domain.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Application.Query;
using Ryzen.Shop.Catalog.Application.Data;

namespace ARyzen.Shop.Catalog.Application.Query;

internal sealed class GetAllroductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductResponse[]>
{
    private readonly ICatalogContext _context;

    public GetAllroductsQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductResponse[]> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Select(p => new ProductResponse(
                p.ProductID,
                p.Name,
                p.Description,
                p.Price,
                p.PromotionID,
                p.ProductPromotion.Type,
                p.ProductPromotion.DiscountAmount,
                p.ProductPromotion.DiscountPercentage,
                p.ProductPromotion.MinimumSpendDiscountAmount,
                p.ProductPromotion.GetOneFree,
                p.ProductPromotion.SecondOneDiscountPercentage
                ))
            .ToArrayAsync(cancellationToken);


        return product;
    }
}
