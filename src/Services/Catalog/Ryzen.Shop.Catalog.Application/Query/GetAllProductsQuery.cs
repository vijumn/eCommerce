using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Ryzen.Shop.Catalog.Application.Query;

namespace Ryzen.Shop.Catalog.Application.Query
{
    public  class GetAllProductsQuery :IRequest<ProductResponse[]>
    {
    }
}
