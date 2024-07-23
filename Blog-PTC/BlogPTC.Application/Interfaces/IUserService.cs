using BlogPTC.Application.Dtos;
using BlogPTC.Domain.Entities;
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
        Task<bool> RegisterUser(RegisterDTO userDto, string password);
        Task UpdateUser(UserDTO userDTO, UserUpdateDTO userUpdateDto);
    }
}
