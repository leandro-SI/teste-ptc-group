using BlogPTC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> GetQuantityUsers();
        Task<UserDTO> GetUserById(string id);
        Task<UserDTO> GetUserByEmail(string email);
        Task<IList<string>> GetRoles(UserDTO userDto);
        Task<bool> CreateUser(RegisterDTO registerDto, string password);
        Task UpdateUser(UserDTO userDTO, UserUpdateDTO userUpdateDto);
    }
}
