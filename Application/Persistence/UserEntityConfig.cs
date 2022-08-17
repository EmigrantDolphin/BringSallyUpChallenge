using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistence;
public class UserEntityConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.Username).HasMaxLength(256).IsRequired();
        builder.Property(x => x.PlainPassword).HasMaxLength(256).IsRequired();

        builder.HasIndex(x => x.Username).IsUnique();
    }
}
