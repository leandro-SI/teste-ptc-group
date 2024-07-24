using BlogPTC.API.Base;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BlogControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly ILogger _logger = null;

        public UserController(IUserService userService, IRoleService roleService, ILogger<UserController> logger)
        {
            _userService = userService;
            _roleService = roleService;
            _logger = logger;
        }


        /// <summary>
        /// Registrar novo usuário
        /// </summary>
        /// <param name="registerDTO">Dados do usuário</param>
        /// <returns>Mensagem de sucesso</returns>
        /// <response code="200">Sucesso</response>
        /// <response code="400">Requisição inválida (por exemplo, dados faltando ou inválidos)</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("register")]
        public async Task<IActionResult> Registrar([FromBody] RegisterDTO registerDTO)
        {

            try
            {
                string funcao = "Administrador";
                if (await _userService.GetQuantityUsers() > 0)
                    funcao = "Usuario";

                var result = await _userService.RegisterUser(registerDTO, registerDTO.Password);

                if (result)
                {
                    var usuario = await _userService.GetUserByEmail(registerDTO.Email);

                    await _roleService.LinkUserRoleAsync(usuario, funcao);

                    return Ok(new
                    {
                        email = usuario.Email,
                        mensagem =  $"Usuário {usuario.UserName} registrado com sucesso. Faça Login!"
                    });
                }

                return BadRequest(registerDTO);
            }
            catch (Exception _erro)
            {
                _logger.LogError(_erro, _erro.Message, null);
                return StatusCodeResponse(StatusCodes.Status500InternalServerError, "Erro ao cadastrar novo usuário!");
            }
        }
    }
}
