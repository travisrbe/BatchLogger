using Contracts;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Service.Abstractions;
using System.Security.Claims;

namespace Services
{
    internal sealed class UserService : IUserService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(
            IRepositoryManager repositoryManager, 
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _repositoryManager = repositoryManager;
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
        public async Task<UserDto> GetUserDetails(string userId, CancellationToken cancellationToken)
        {
            User user = await _repositoryManager.UserRepository.GetByIdAsync(userId, cancellationToken)
                ?? throw new UserNotFoundException(userId);
            return user.Adapt<UserDto>();
        }
    }
}
