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
    public class AuthenticateController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        private readonly IAuthenticate _authenticateService;
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AuthenticateController(ITokenService tokenService, IAuthenticate authenticateService, IUserService userService, IRoleService roleService)
        {
            _tokenService = tokenService;
            _authenticateService = authenticateService;
            _userService = userService;
            _roleService = roleService;
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
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}
