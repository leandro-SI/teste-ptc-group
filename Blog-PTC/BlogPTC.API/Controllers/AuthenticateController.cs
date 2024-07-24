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


        /// <summary>
        /// Logar e gerar token de usuário
        /// </summary>
        /// <param name="loginDto">Dados do usuário</param>
        /// <returns>Token</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="401">Usuário ñão autorizado</response>
        /// <response code="500">Erro interno do servidor</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("login")]
        public async Task<IActionResult> Logar([FromBody] LoginDTO loginDto)
        {
            try
            {

                var user = await _userService.GetUserByEmail(loginDto.Email);

                if (user != null)
                {
                    PasswordHasher<UserDTO> passwordHasher = new PasswordHasher<UserDTO>();
                    var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, loginDto.Password);

                    if (result != PasswordVerificationResult.Failed)
                    {
                        var userRoles = await _roleService.GetRolesByUser(user);
                        var tokenUser = _tokenService.GenerateToken(user, userRoles.First());

                        return Ok(new
                        {
                            username = user.UserName,
                            mensagem = "Login realizado com sucesso.",
                            token = tokenUser
                        });
                    }

                }

                return Unauthorized("Usuário ou senha inválidos.");
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao realizar login!");
            }
        }
    }
}
