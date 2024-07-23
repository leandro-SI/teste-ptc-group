using BlogPTC.Domain.Entities;

namespace BlogPTC.Domain.Account
{
    public interface IAuthenticate
    {
        Task<bool> RegisterUserAsync(User user, string password);
        Task<User> GetUserByEmailAsync(string email);
        Task LinkUserRoleAsync(User user, string role);
        Task<IList<string>> GetRolesAsync(User user);
    }
}
