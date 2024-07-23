using BlogPTC.Domain.Entities;

namespace BlogPTC.Domain.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPostByIdAsync(long id);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task CreatePostAsync(Post post);
        Task UpdatePostAsync(Post post);
        Task DeletePostAsync(long id);
    }
}
