using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost("new")]
        public async Task<IActionResult> Create([FromBody] NewPostDTO postDto)
        {

            var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "user_id").Value;
            postDto.UserId = userId;

            await _postService.CreatePost(postDto);

            return Ok(new { mensagem = $"post {postDto.Title} criado com sucesso." });
        }

    }
}
