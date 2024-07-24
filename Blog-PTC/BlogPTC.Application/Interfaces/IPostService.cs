using BlogPTC.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDTO>> GetAllPosts();
        Task<PostDTO> GeTPostById(long id);
        Task UpdatePost(PostDTO post, UpdatePostDTO postDto);
        Task CreatePost(NewPostDTO postDto);
        Task DeletePost(long id);
    }
}
