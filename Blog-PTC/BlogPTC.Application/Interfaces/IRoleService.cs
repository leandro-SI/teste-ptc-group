using BlogPTC.Application.Dtos;
using BlogPTC.Domain.Entities;

namespace BlogPTC.Application.Interfaces
{
    public interface IRoleService
    {
        Task LinkUserRoleAsync(UserDTO user, string role);
        Task<IList<string>> GetRolesByUser(UserDTO userDto);
    }
}
