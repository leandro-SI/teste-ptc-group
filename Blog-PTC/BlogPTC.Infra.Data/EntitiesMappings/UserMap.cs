using BlogPTC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPTC.Infra.Data.EntitiesMappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Id).ValueGeneratedOnAdd();

            builder.HasMany(u => u.Posts)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            builder.ToTable("Usuarios");
        }
    }
}
