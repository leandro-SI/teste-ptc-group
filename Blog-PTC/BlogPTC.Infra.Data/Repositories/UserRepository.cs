using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;
using BlogPTC.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogPTC.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetQuantityUserAsync()
        {
            return await _context.Usuarios.CountAsync();
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public Task<User> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByUsernameAsync(string username)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userManager.UpdateAsync(user);
        }
    }
}
