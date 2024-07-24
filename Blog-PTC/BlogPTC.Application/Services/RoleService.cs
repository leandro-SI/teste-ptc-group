using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;

namespace BlogPTC.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        public async Task<IList<string>> GetRolesByUser(UserDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            return await _roleRepository.GetRolesByUserAsync(user);
        }

        public async Task LinkUserRoleAsync(UserDTO userDto, string role)
        {
            userDto.SecurityStamp = Guid.NewGuid().ToString();
            var user = _mapper.Map<User>(userDto);

            await _roleRepository.LinkUserRoleAsync(user, role);
        }
    }
}
