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
    public class UsersController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;
        private readonly string _userId;

        public UsersController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
            _userId = GetUserId();
        }
        private string GetUserId()
        {
            return _serviceManager.UserService.GetUserId();
        }

        [HttpGet]
        [Route("Current")]
        public async Task<ActionResult> UserDetails(CancellationToken cancellationToken)
        {
            UserDto userDto = await _serviceManager.UserService.GetUserDetails(_userId, cancellationToken);
            return Ok(userDto);
        }
    }
}
