using Microsoft.AspNetCore.Identity;

namespace BlogPTC.Domain.Entities
{
    public class User : IdentityUser<string>
    {
        public ICollection<Post> Posts { get; set; }
    }
}
