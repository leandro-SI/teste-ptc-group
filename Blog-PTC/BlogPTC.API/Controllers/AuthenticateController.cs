using BlogPTC.API.Base;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
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

                var user = await _userService.GetUserByEmail(loginDto.Email);

                if (user != null)
                {
                    PasswordHasher<UserDTO> passwordHasher = new PasswordHasher<UserDTO>();
                    if (passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password) != PasswordVerificationResult.Failed)
                    {
                        var userRoles = await _roleService.GetRolesByUser(user);

                        var tokenUser = _tokenService.GenerateToken(user, userRoles.First());

                        return Ok(new
                        {
                            username = user.UserName,
                            mensagem = "Login feito com sucesso.",
                            token = tokenUser
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
