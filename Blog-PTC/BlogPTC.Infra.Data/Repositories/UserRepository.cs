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

        public async Task<bool> RegisterUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            _context.Entry(user).State = EntityState.Detached;

            return result.Succeeded;
        }

        public async Task<int> GetQuantityUserAsync()
        {
            return await _context.Usuarios.CountAsync();
        }


        public async Task<User> GetUserByEmailAsync(string email)
        {
            var user = await _context.Usuarios.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }

    }
}
