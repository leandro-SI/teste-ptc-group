using BlogPTC.API.Base;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Application.WebSockets.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : BlogControllerBase
    {
        private readonly IPostService _postService;
        private readonly INotificationService _notificationService;
        private readonly ILogger _logger = null;

        public PostController(IPostService postService, ILogger<PostController> logger, INotificationService notificationService)
        {
            _postService = postService;
            _logger = logger;
            _notificationService = notificationService;
        }


        /// <summary>
        /// Recuperar todos os posts
        /// </summary>
        /// <returns>Lista de posts</returns>
        /// <response code="200">Lista de posts retornada com sucesso</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Criar novo post
        /// </summary>
        /// <param name="postDto">Dados do post</param>
        /// <returns>Mensagem de sucesso</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize(Roles = "Administrador, Usuario")]
        [HttpPost("new")]
        public async Task<IActionResult> CreatePost([FromBody] NewPostDTO postDto)
        {
            try
            {
                var userId = HttpContext.User.Claims.FirstOrDefault(u => u.Type == "user_id").Value;
                postDto.UserId = userId;

                await _postService.CreatePost(postDto);

                await _notificationService.SendNotificationAsync($"Novo post criado: {postDto.Title}");

                return Ok(new { mensagem = $"post {postDto.Title} criado com sucesso." });
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao cadastra novo post!");
            }
        }


        /// <summary>
        /// Atualizar um post existente
        /// </summary>
        /// <param name="id">ID do post a ser atualizado</param>
        /// <param name="postUpdateDto">Dados atualizados do post</param>
        /// <returns>Mensagem de sucesso</returns>
        /// <response code="200">Post atualizado com sucesso</response>
        /// <response code="400">Requisição inválida (por exemplo, dados faltando ou inválidos)</response>
        /// <response code="404">Post não encontrado</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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


        /// <summary>
        /// Excluir um post existente
        /// </summary>
        /// <param name="id">ID do post a ser excluído</param>
        /// <returns>Mensagem de sucesso</returns>
        /// <response code="200">Post excluído com sucesso</response>
        /// <response code="404">Post não encontrado</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
