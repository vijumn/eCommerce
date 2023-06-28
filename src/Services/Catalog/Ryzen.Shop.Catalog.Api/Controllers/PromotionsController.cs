
using MediatR;
using Ryzen.Shop.Catalog.Application.Query;


namespace Ryzen.Shop.Catalog.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PromotionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductsController> _logger;

        public PromotionsController(ILogger<ProductsController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [Route("Search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get([FromQuery] int[] ids)
        {
            var commandResult = await _mediator.Send(new GetPromotionsQuery(ids));
            return Ok(commandResult);
           
        }
      
        
    }
}
