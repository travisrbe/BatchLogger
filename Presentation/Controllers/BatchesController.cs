﻿using Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Abstractions;

namespace Presentation.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BatchesController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly string _userId;

        public BatchesController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _userId = GetUserId();
        }

        private string GetUserId()
        {
            return _serviceManager.UserService.GetUserId();
        }

        //[HttpGet]
        //[Authorize(Roles = "Administrator")]
        //public async Task<IActionResult> Get(CancellationToken cancellationToken)
        //{
        //    var BatchesDto = await _serviceManager.BatchService.GetAllAsync(cancellationToken);
        //    return Ok(BatchesDto);
        //}

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var BatchDto = await _serviceManager.BatchService.GetByIdAsync(_userId, id, cancellationToken);
            return Ok(BatchDto);
        }

        [HttpGet]
        [Route("MyBatches")]
        public async Task<IActionResult> GetUserBatches(CancellationToken cancellationToken)
        {
            var BatchesDto = await _serviceManager.BatchService.GetByUserIdAsync(_userId, cancellationToken);
            return Ok(BatchesDto);
        }

        [HttpGet]
        [Route("OwnedBatches")]
        public async Task<IActionResult> GetOwnedBatches(CancellationToken cancellationToken)
        {
            var BatchesDto = await _serviceManager.BatchService.GetByOwnedAsync(_userId, cancellationToken);
            return Ok(BatchesDto);
        }

        [HttpPost]
        [Route("Update")]
        public async Task<IActionResult> Update([FromBody] BatchDto batchDto, CancellationToken cancellationToken)
        {
            var updatedBatchDto = await _serviceManager.BatchService.Update(_userId, batchDto, cancellationToken);
            return Ok(updatedBatchDto);
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create([FromBody] BatchDto batchDto, CancellationToken cancellationToken)
        {
            //This endpoint needs optimizing.

            //create the batch
            batchDto.OwnerUserId = _userId;
            var SavedBatchDto = await _serviceManager.BatchService.Create(batchDto, cancellationToken);

            //create a userbatch
            UserBatchDto userBatchDto = new UserBatchDto() 
            { 
                BatchId = batchDto.Id,
                UserId = _userId
            };
            var UserBatchDto = await _serviceManager.UserBatchService.Create(_userId, SavedBatchDto.Id, cancellationToken);

            return Ok(SavedBatchDto);
        }
        [HttpPost]
        [Route("Delete")]
        public async Task<IActionResult> Delete([FromBody] BatchDto batchDto, CancellationToken cancellationToken)
        {
            var BatchDto = await _serviceManager.BatchService.Delete(_userId, batchDto, cancellationToken);
            return Ok(BatchDto);
        }
    }
}