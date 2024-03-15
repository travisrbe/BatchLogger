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
        public async Task<IActionResult> Create(Guid batchId, Guid nutrientId, int priority, CancellationToken cancellationToken)
        {
            var NutrientAdditionDto = await _serviceManager.NutrientAdditionService
                .Create(_userId, batchId, nutrientId, priority, cancellationToken);
            return Ok(NutrientAdditionDto);
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
        [Route("UpdateAll")]
        public async Task<IActionResult> UpdateAll([FromBody] IEnumerable<NutrientAdditionDto> nutrientAdditionDtos, CancellationToken cancellationToken)
        {
            nutrientAdditionDtos = await _serviceManager.NutrientAdditionService
                .UpdateRange(_userId, nutrientAdditionDtos, cancellationToken);
            return Ok(nutrientAdditionDtos);
        }

        [HttpPost]
        [Route("Reset")]
        public async Task<IActionResult> Reset([FromBody] IEnumerable<NutrientAdditionDto> nutrientAdditionDtos, CancellationToken cancellationToken)
        {
            if (nutrientAdditionDtos.Count() > 0)
            {
                nutrientAdditionDtos = await _serviceManager.NutrientAdditionService
                .Reset(_userId, nutrientAdditionDtos, cancellationToken);
            }
            return Ok(nutrientAdditionDtos);
        }

        [HttpPost]
        [Route("StackPreset")]
        public async Task<IActionResult> StackPreset(Guid batchId, Guid stackPresetId, CancellationToken cancellationToken)
        {
            IEnumerable<NutrientAdditionDto> nutrientAdditionDtos = await _serviceManager.NutrientAdditionService
                .SetStackPreset(_userId, batchId, stackPresetId, cancellationToken);
            return Ok(nutrientAdditionDtos);
        }

        [HttpPost]
        [Route("RestoreDefaultValues")]
        public async Task<IActionResult> RestoreDefaultValues([FromBody] NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            var result = await _serviceManager.NutrientAdditionService
                .RestoreDefaultValues(_userId, nutrientAdditionDto, cancellationToken);
            return Ok(result);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] NutrientAdditionDto nutrientAdditionDto, CancellationToken cancellationToken)
        {
            await _serviceManager.NutrientAdditionService
                .Delete(_userId, nutrientAdditionDto, cancellationToken);
            return Ok();
        }
    }
}
