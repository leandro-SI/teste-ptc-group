﻿using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Dtos.Responses;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;

namespace BlogPTC.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public async Task CreatePost(NewPostDTO postDto)
        {
            var post = _mapper.Map<Post>(postDto);
            post.CreatedAt = DateTime.Now;
            post.UpdatedAt = DateTime.Now;

            await _postRepository.CreatePostAsync(post);
        }

        public async Task DeletePost(long id)
        {
            await _postRepository.DeletePostAsync(id);
        }

        public async Task<PostDTO> GeTPostById(long id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);

            return _mapper.Map<PostDTO>(post);
        }

        public async Task<IEnumerable<PostResponseDTO>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            var postsResult = posts.Select(post => new PostResponseDTO
            {
                Id = post.Id,
                Title = post.Title,
                CreatedAt = post.CreatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
                UpdatedAt = post.UpdatedAt.ToString("dd-MM-yyyy HH:mm:ss"),
                Content = post.Content,
                CreatedBy = post.User.UserName
            }).ToList();

            return postsResult;
        }

        public async Task UpdatePost(PostDTO post, UpdatePostDTO postUpdateDto)
        {
            post.Title = postUpdateDto.Title;
            post.Content = postUpdateDto.Content;
            post.UpdatedAt = DateTime.Now;

            var postEdit = _mapper.Map<Post>(post);

            await _postRepository.UpdatePostAsync(postEdit);
        }
    }
}
