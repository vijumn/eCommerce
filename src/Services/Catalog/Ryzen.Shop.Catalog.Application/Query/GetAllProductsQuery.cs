
using MediatR;
namespace Ryzen.Shop.Catalog.Application.Query
{
    public  class GetAllProductsQuery :IRequest<ProductResponse[]>
    {
    }
}
