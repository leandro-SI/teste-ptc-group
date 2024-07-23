using Microsoft.AspNetCore.Identity;

namespace BlogPTC.Domain.Entities
{
    public sealed class User : IdentityUser<string>
    {
        public ICollection<Post> Posts { get; set; }
    }
}
