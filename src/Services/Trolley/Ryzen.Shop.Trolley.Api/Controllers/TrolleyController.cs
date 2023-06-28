using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ryzen.Shop.Trolley.Api.Services;

namespace Ryzen.Shop.Trolley.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly ILogger<TrolleyController> _logger;
        private readonly ITrolleyService _trolleyService;

        public TrolleyController(ILogger<TrolleyController> logger, ITrolleyService trolleyService)
        {
            _logger = logger;
            _trolleyService = trolleyService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Get(string customerId)
        {
            var trolley = await _trolleyService.GetTrolley(customerId); 
            return Ok();
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Update(string customerId, CustomerTrolley customerTrolley)
        {
            var trolley = await _trolleyService.UpdateTrolley(customerId, customerTrolley);
            return Ok(trolley);
        }
    }
}
