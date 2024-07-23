using AutoMapper;
using BlogPTC.Application.Dtos;
using BlogPTC.Application.Interfaces;
using BlogPTC.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public TokenService(IConfiguration configuration, IMapper mapper)
        {
            _configuration = configuration;
            _mapper = mapper;
        }

        public string GenerateToken(UserDTO userDto, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secret = _configuration["Jwt:SecretKey"];
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                    new Claim("user_id", userDto.Id),
                    new Claim(ClaimTypes.Name, userDto.UserName),
                    new Claim(ClaimTypes.Role, role)
                }),

                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }
    }
}
