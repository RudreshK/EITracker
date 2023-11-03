using EITracker.Models;

namespace EITracker.UI.Services
{
    public interface IUserService
    {
        public Task<List<UserModel>> GetAllUsersAsync();
        public Task<UserModel> GetUserByIdAsync(Guid userId);
        public Task PatchUserAsync(UserModel user, CancellationToken token);

    }
}
