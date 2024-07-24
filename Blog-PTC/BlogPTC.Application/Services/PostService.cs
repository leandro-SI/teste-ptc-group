﻿using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Entities;
using BlogPTC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<PostDTO>> GetAllPosts()
        {
            var posts = await _postRepository.GetAllPostsAsync();

            return _mapper.Map<IEnumerable<PostDTO>>(posts);
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
