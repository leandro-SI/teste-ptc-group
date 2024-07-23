using BlogPTC.Application.Dtos;
using BlogPTC.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogPTC.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(UserDTO userDto, string role);
    }
}
