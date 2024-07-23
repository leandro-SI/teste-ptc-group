using BlogPTC.Application.Dtos;

namespace BlogPTC.Application.Interfaces
{
    public interface IRoleService
    {
        Task LinkUserRoleAsync(UserDTO user, string role);
    }
}
