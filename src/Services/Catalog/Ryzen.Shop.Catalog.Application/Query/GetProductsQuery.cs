
using MediatR;
namespace Ryzen.Shop.Catalog.Application.Query
{
    public  record GetProductsQuery(int[] ProductIds) : IRequest<ProductResponse[]>;

    public record ProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Amount
    );

}
