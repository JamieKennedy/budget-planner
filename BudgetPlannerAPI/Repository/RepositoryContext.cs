using Common.Models;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define unique index for user based on ClerkId
        modelBuilder.Entity<User>()
            .HasIndex(user => user.ClerkId)
            .IsUnique();
    }

    public DbSet<User>? Users { get; set; }
    public DbSet<Savings>? Savings { get; set; }
    public DbSet<SavingsBalance>? SavingsBalance { get; set; }
    public DbSet<Group>? Groups { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
}