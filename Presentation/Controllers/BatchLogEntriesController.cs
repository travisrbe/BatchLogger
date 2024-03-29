﻿using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BatchLogEntriesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly string _userId;

        public BatchLogEntriesController(IServiceManager serviceManager)
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
        public async Task<IActionResult> Create([FromBody] Guid batchId, CancellationToken cancellationToken)
        {
            var BatchLogEntryDto = await _serviceManager.BatchLogEntryService
                .Create(_userId, batchId, cancellationToken);
            return Ok(BatchLogEntryDto);
        }

        [HttpPost]
        [Route("Import")]
        public async Task<IActionResult> Import([FromBody] IEnumerable<NutrientAdditionDto> nuAdds, CancellationToken cancellationToken)
        {
            var BatchLogEntryDto = await _serviceManager.BatchLogEntryService
                .Create(_userId, nuAdds, cancellationToken);
            return Ok(BatchLogEntryDto);
        }

        [HttpGet]
        [Route("Batch/{batchId:guid}")]
        public async Task<IActionResult> GetByBatchId(Guid batchId, CancellationToken cancellationToken)
        {
            var nutrientAdditionsDto = await _serviceManager.BatchLogEntryService
                .GetBatchLogEntries(_userId, batchId, cancellationToken);
            return Ok(nutrientAdditionsDto);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken)
        {
            var BatchLogEntryDto = await _serviceManager.BatchLogEntryService
                .Update(_userId, batchLogEntryDto, cancellationToken);
            return Ok(BatchLogEntryDto);
        }

        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken)
        {
            var BatchLogEntryDto = await _serviceManager.BatchLogEntryService
                .Delete(_userId, batchLogEntryDto, cancellationToken);
            return Ok(BatchLogEntryDto);
        }
    }
}
