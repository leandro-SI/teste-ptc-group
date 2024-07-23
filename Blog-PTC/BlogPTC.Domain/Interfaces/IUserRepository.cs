using BlogPTC.Domain.Entities;

namespace BlogPTC.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(long id);
        Task<User> GetUserByUsernameAsync(string username);
        Task UpdateUserAsync(User user);
        Task CreateUserAsync(User user);
        Task<int> GetQuantityUserAsync();
    }
}
