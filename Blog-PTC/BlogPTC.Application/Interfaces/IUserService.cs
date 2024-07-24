using BlogPTC.Application.Dtos;

namespace BlogPTC.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> GetQuantityUsers();
        Task<UserDTO> GetUserByEmail(string email);
        Task<bool> RegisterUser(RegisterDTO userDto, string password);
    }
}
