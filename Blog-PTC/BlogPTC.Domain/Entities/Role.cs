using Microsoft.AspNetCore.Identity;

namespace BlogPTC.Domain.Entities
{
    public class Role : IdentityRole<string>
    {
        public string Descricao { get; set; }
    }
}
