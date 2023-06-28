using MediatR;
using Microsoft.EntityFrameworkCore;
using Ryzen.Shop.Catalog.Application.Data;

namespace Ryzen.Shop.Catalog.Application.Query;

internal sealed class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, ProductResponse[]>
{
    private readonly ICatalogContext _context;

    public GetProductsQueryHandler(ICatalogContext context)
    {
        _context = context;
    }

    public async Task<ProductResponse[]> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var product = await _context
            .Products
            .Where( p => request.ProductIds.Contains(p.ProductID))
            .Select(p => new ProductResponse(
                p.ProductID,
                p.Name,
                p.Description,
                p.Price
              
                ))
            .ToArrayAsync(cancellationToken);


        return product;
    }
}
