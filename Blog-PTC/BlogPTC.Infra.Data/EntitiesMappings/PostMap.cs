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
                .HasMaxLength(80);

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("GETDATE()");

            builder.HasOne(p => p.User)
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.UserId)
                .IsRequired();

            builder.ToTable("Posts");
        }
    }
}
