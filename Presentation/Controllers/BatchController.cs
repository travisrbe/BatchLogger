using Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BatchController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public BatchController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetBatches(CancellationToken cancellationToken)
        {
            var BatchesDto = await _serviceManager.BatchService.GetAllAsync(cancellationToken);
            return Ok(BatchesDto);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var BatchDto = await _serviceManager.BatchService.GetByIdAsync(id, cancellationToken);
            return Ok(BatchDto);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOwner([FromBody] BatchDto batchDto, CancellationToken cancellationToken)
        {
            //ClaimsPrincipal currentUser = this.User;
            //var currentUserId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;

            var userId = _serviceManager.UserService.GetUserId();
            batchDto.CreatorUserId = userId;
            var BatchDto = await _serviceManager.BatchService.Create(batchDto, cancellationToken);
            return Ok(BatchDto);
        }
    }
}
