using BlogPTC.Domain.Entities;

namespace BlogPTC.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User> GetUserByEmailAsync(string email);
        Task UpdateUserAsync(User user);
        Task CreateUserAsync(User user);
        Task<int> GetQuantityUserAsync();
        Task<IList<string>> GetRolesAsync(User user);
    }
}
