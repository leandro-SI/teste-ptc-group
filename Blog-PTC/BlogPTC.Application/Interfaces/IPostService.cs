using BlogPTC.Application.Dtos;
using BlogPTC.Application.Dtos.Responses;

namespace BlogPTC.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostResponseDTO>> GetAllPosts();
        Task<PostDTO> GeTPostById(long id);
        Task UpdatePost(PostDTO post, UpdatePostDTO postDto);
        Task CreatePost(NewPostDTO postDto);
        Task DeletePost(long id);
    }
}
