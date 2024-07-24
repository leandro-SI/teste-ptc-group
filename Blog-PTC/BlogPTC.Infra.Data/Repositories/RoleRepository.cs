using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BlogPTC.Infra.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<User> _userManager;

        public RoleRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IList<string>> GetRolesByUserAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task LinkUserRoleAsync(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
