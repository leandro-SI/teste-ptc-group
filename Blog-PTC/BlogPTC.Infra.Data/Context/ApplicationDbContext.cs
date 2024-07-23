using BlogPTC.Domain.Entities;
using BlogPTC.Infra.Data.EntitiesMappings;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogPTC.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public DbSet<User> Usuarios { get; set; }
        public DbSet<Role> Funcoes { get; set; }
        public DbSet<Post> Posts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new PostMap());
            builder.ApplyConfiguration(new RoleMap());
            builder.ApplyConfiguration(new UserMap());

        }

    }
}
