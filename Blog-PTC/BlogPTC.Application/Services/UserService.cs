using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;

namespace BlogPTC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
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

            return await _userRepository.RegisterUserAsync(user, password);
        }

        public async Task<int> GetQuantityUsers()
        {
            return await _userRepository.GetQuantityUserAsync();
        }

        public async Task<UserDTO> GetUserByEmail(string email)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);

            return _mapper.Map<UserDTO>(user);
        }


    }
}
