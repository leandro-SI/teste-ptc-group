using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Infra.Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly UserManager<User> _userManager;

        public RoleRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task LinkUserRoleAsync(User user, string role)
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }
}
