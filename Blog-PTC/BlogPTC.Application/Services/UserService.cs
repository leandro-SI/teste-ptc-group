using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Account;
using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticate _authenticateService;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IAuthenticate authenticateService, IMapper mapper)
        {
            _userRepository = userRepository;
            _authenticateService = authenticateService;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUser(RegisterDTO registerDto, string password)
        {
            var userDto = new UserDTO
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password
            };

            var user = _mapper.Map<User>(userDto);

            return await _authenticateService.RegisterUserAsync(user, password);
        }

        public async Task<int> GetQuantityUsers()
        {
            return await _userRepository.GetQuantityUserAsync();
        }

        public async Task<IList<string>> GetRoles(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);

            return await _userRepository.GetRolesAsync(user);
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task<UserDTO> GetUserById(string id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task UpdateUser(UserDTO userDTO, UserUpdateDTO userUpdateDto)
        {
            userDTO.UserName = userUpdateDto.UserName;
            userDTO.Email = userUpdateDto.Email;

            var newUser = _mapper.Map<User>(userDTO);

            await _userRepository.UpdateUserAsync(newUser);
        }


    }
}
