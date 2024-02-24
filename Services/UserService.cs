using Microsoft.AspNetCore.Http;
using Service.Abstractions;
using System.Security.Claims;

namespace Services
{
    internal sealed class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var id = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            if (id == null)
            {
                throw new Exception("No logged in user detected");
            }
            return id;
        }
    }
}
