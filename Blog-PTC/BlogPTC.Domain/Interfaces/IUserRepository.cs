using BlogPTC.Domain.Entities;

namespace BlogPTC.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<int> GetQuantityUserAsync();
        Task<bool> RegisterUserAsync(User user, string password);        
    }
}
