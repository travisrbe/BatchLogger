using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NutrientsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public NutrientsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var nutrientsDto = await _serviceManager.NutrientService.GetAllAsync(cancellationToken);

            return Ok(nutrientsDto);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var nutrientDto = await _serviceManager.NutrientService.GetByIdAsync(id, cancellationToken);
            return Ok(nutrientDto);
        }
    }
}
