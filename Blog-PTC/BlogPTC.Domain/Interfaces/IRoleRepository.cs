using BlogPTC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Domain.Interfaces
{
    public interface IRoleRepository
    {
        Task LinkUserRoleAsync(User user, string role);
        Task<IList<string>> GetRolesByUserAsync(User user);
    }
}
