using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ryzen.Shop.Trolley.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private readonly ILogger<TrolleyController> _logger;

        public TrolleyController(ILogger<TrolleyController> logger)
        {
            _logger = logger;
        }

        
    }
}
