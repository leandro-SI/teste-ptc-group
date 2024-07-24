using BlogPTC.API.Base;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : BlogControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILogger _logger = null;

        public AuthenticateController(ITokenService tokenService, IUserService userService, IRoleService roleService, ILogger<AuthenticateController> logger)
        {
            _tokenService = tokenService;
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Logar([FromBody] LoginDTO loginDto)
        {
            try
            {
                if (loginDto == null)
                    return NotFound("Usuario ou senha inválidos.");

                var usuario = await _userService.GetUserByEmail(loginDto.Email);

                if (usuario != null)
                {
                    PasswordHasher<UserDTO> passwordHasher = new PasswordHasher<UserDTO>();
                    if (passwordHasher.VerifyHashedPassword(usuario, usuario.PasswordHash, loginDto.Password) != PasswordVerificationResult.Failed)
                    {
                        var funcoesUsuario = await _roleService.GetRolesByUser(usuario);

                        var tokenUsuario = _tokenService.GenerateToken(usuario, funcoesUsuario.First());

                        return Ok(new
                        {
                            username = usuario.UserName,
                            mensagem = "Login feito com sucesso.",
                            token = tokenUsuario
                        });
                    }
                    return NotFound("Usuário inválido.");

                }

                return NotFound("Usuário inválido.");
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao realizar login!");
            }
        }
    }
}
