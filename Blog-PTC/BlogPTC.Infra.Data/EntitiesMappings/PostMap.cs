using BlogPTC.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPTC.Infra.Data.EntitiesMappings
{
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(80);

            builder.HasIndex(p => p.Title).IsUnique();

            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(400);

            builder.HasOne(p => p.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder.ToTable("Posts");
        }
    }
}
