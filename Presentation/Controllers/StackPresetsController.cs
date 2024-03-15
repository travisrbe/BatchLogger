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
    public class StackPresetsController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public StackPresetsController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult> GetAll(CancellationToken cancellationToken)
        {
            IEnumerable<StackPresetDto> stackPresetsDto = await _serviceManager.StackPresetService.GetOrderedStackPresets(cancellationToken);
            return Ok(stackPresetsDto);
        }
    }
}
