using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Persistence;
public class BimboContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Challenge> Challenges { get; set; }

    public BimboContext(DbContextOptions<BimboContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(UserEntityConfig).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ChallengeEntityConfig).Assembly);
    }
}
