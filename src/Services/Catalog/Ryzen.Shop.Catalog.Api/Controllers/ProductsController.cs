
using MediatR;
using Ryzen.Shop.Catalog.Application.Query;

namespace Ryzen.Shop.Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }



        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(int id)
        {
            var commandResult = await _mediator.Send(new GetProductQuery(id));
            return Ok(commandResult);
           
        }
        [HttpGet]
        [Route("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var commandResult = await _mediator.Send(new GetAllProductsQuery());
            return Ok(commandResult);
        }
        
    }
}
