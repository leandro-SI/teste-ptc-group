using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;
using BlogPTC.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Infra.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreatePostAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePostAsync(long id)
        {
            var post = await GetPostByIdAsync(id);
            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            var posts = await _context.Posts.Include(p => p.User).ToListAsync();
            return posts;
        }

        public async Task<Post> GetPostByIdAsync(long id)
        {
            var post = await _context.Posts.FindAsync(id);
            return post;
        }

        public async Task UpdatePostAsync(Post post)
        {
            _context.Posts.Update(post);
            await _context.SaveChangesAsync();
        }
    }
}
