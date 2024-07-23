﻿using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Application.Services;
using BlogPTC.Domain.Account;
using BlogPTC.Infra.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogPTC.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticate _authenticate;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IAuthenticate authenticate, IConfiguration configuration, IRoleService roleService)
        {
            _userService = userService;
            _authenticate = authenticate;
            _configuration = configuration;
            _roleService = roleService;
        }

        [HttpPost("registrar")]
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
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }
        }
    }
}
