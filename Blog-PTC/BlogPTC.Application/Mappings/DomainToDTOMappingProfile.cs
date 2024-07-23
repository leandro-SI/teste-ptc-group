using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Domain.Entities;

namespace BlogPTC.Application.Mappings
{
    public class DomainToDTOMappingProfile : Profile
    {
        public DomainToDTOMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Post, NewPostDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
