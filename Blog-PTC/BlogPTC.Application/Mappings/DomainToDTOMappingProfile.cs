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
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss")))
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt.ToString("yyyy-MM-dd HH:mm:ss")));
            CreateMap<Post, NewPostDTO>().ReverseMap();
            CreateMap<Post, UpdatePostDTO>().ReverseMap().ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            CreateMap<Role, RoleDTO>().ReverseMap();
        }
    }
}
