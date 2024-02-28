using Contracts;

namespace Service.Abstractions
{
    public interface IUserService
    {
        string GetUserId();
        Task<UserDto> GetUserDetails(string userId, CancellationToken cancellationToken);
    }
}