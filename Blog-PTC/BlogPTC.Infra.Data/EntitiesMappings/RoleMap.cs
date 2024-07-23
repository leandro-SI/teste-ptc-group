using BlogPTC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPTC.Infra.Data.EntitiesMappings
{
    public class RoleMap : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(r => r.Id).ValueGeneratedOnAdd();

            builder.Property(r => r.Descricao)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasData(
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Administrador",
                    NormalizedName = "ADMINISTRADOR",
                    Descricao = "Administrador do blog"
                },
                new Role
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Usuario",
                    NormalizedName = "USUARIO",
                    Descricao = "Usuário do blog"
                });

            builder.ToTable("Funcoes");
        }
    }
}
