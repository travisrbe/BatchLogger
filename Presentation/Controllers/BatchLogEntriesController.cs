using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Create([FromBody] BatchLogEntryDto batchLogEntryDto, CancellationToken cancellationToken)
        {
            var BatchLogEntryDto = await _serviceManager.BatchLogEntryService
                .Create(_userId, batchLogEntryDto, cancellationToken);
            return Ok(BatchLogEntryDto);
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
