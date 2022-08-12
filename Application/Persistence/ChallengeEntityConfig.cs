using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Application.Persistence;
public class ChallengeEntityConfig : IEntityTypeConfiguration<Challenge>
{
    public void Configure(EntityTypeBuilder<Challenge> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.Property(x => x.UserId).IsRequired();
        builder.Property(x => x.Seconds).IsRequired();
        builder.Property(x => x.Improvement).IsRequired(false);
        builder.Property(x => x.Comment).HasMaxLength(2000).IsRequired(false);

        builder.HasOne(x => x.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade); // my project, I do what I want.
    }
}
