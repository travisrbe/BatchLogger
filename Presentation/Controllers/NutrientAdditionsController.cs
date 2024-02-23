using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class NutrientAdditionsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly string _userId;

        public NutrientAdditionsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _userId = GetUserId();
        }

        private string GetUserId()
        {
            return _serviceManager.UserService.GetUserId();
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            var NutrientAdditionDto = await _serviceManager.NutrientAdditionService
                .Create(_userId, nutrientAdditionDto, cancellationToken);
            return Ok(nutrientAdditionDto);
        }

        [HttpGet]
        [Route("Batch/{batchId:guid}")]
        public async Task<IActionResult> GetByBatchId(Guid batchId, CancellationToken cancellationToken)
        {
            var nutrientAdditionsDto = await _serviceManager.NutrientAdditionService
                .GetByBatchId(_userId, batchId, cancellationToken);
            return Ok(nutrientAdditionsDto);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            var NutrientAdditionDto = await _serviceManager.NutrientAdditionService
                .Update(_userId, nutrientAdditionDto, cancellationToken);
            return Ok(nutrientAdditionDto);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            var NutrientAdditionDto = await _serviceManager.NutrientAdditionService
                .Delete(_userId, nutrientAdditionDto, cancellationToken);
            return Ok(nutrientAdditionDto);
        }
    }
}
