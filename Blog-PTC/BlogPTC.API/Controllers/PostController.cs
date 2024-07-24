using BlogPTC.API.Base;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BlogControllerBase
    {
        private readonly IPostService _postService;
        private readonly ILogger _logger = null;

        public PostController(IPostService postService, ILogger<PostController> logger)
        {
            _postService = postService;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllPosts()
        {
            try
            {
                var posts = await _postService.GetAllPosts();

                return Ok(posts);
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao listar os posts!");
            }
        }

        [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost("new")]
        public async Task<IActionResult> CreatePost([FromBody] NewPostDTO postDto)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "user_id").Value;
                postDto.UserId = userId;

                await _postService.CreatePost(postDto);

                return Ok(new { mensagem = $"post {postDto.Title} criado com sucesso." });
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao cadastra novo post!");
            }
        }

        [Authorize(Roles = "Administrador, Usuario")]
        [HttpPut("edit/{id}")]
        public async Task<IActionResult> EditPost([FromBody] UpdatePostDTO postUpdateDto, long id)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "user_id").Value;
                postUpdateDto.UserId = userId;

                if (id != postUpdateDto.Id)
                    return BadRequest("Invalid Data");

                var post = await _postService.GeTPostById(postUpdateDto.Id);

                if (post == null)
                    return NotFound("Post não encontrado.");

                if (post.UserId != userId)
                    return BadRequest("Você só pode alterar o seu próprio post!");

                await _postService.UpdatePost(post, postUpdateDto);

                return Ok(new { mensagem = $"post {postUpdateDto.Title} alterado com sucesso." });
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao cadastra novo post!");
            }
        }

        [Authorize(Roles = "Administrador, Usuario")]
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeletePost(long id)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "user_id").Value;

                var post = await _postService.GeTPostById(id);

                if (post == null)
                    return NotFound("Post não encontrado.");

                if (post.UserId != userId)
                    return BadRequest("Você só pode excluir o seu próprio post!");

                await _postService.DeletePost(id);

                return Ok(new { mensagem = $"post {post.Title} excluido com sucesso." });
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao tentar excluir post!");
            }
        }

    }
}
