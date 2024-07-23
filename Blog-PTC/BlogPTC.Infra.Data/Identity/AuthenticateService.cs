using BlogPTC.Domain.Account;
using BlogPTC.Domain.Entities;
using BlogPTC.Infra.Data.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Infra.Data.Identity
{
    public class AuthenticateService : IAuthenticate
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _applicationDbContext;

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userManager.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task LinkUserRoleAsync(User user, string role)
        {
            var result = await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<bool> RegisterUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);

            return result.Succeeded;
        }
    }
}
