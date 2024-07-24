using Microsoft.AspNetCore.Identity;

namespace BlogPTC.Application.Dtos
{
    public class RoleDTO : IdentityRole<string>
    {
        public string Descricao { get; set; }
    }
}
