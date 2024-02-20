using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")] 
    public class YeastsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public YeastsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var yeastsDto = await _serviceManager.YeastService.GetAllAsync(cancellationToken);

            return Ok(yeastsDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var yeastDto = await _serviceManager.YeastService.GetByIdAsync(id, cancellationToken);
            return Ok(yeastDto);
        }
    }
}
