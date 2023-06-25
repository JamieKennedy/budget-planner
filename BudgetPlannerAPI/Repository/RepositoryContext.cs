using Common.Models.Saving;
using Common.Models.SavingBalance;
using Common.Models.User;

using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : DbContext
{
    public RepositoryContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Define unique index for user based on ClerkId
        modelBuilder.Entity<UserModel>()
            .HasIndex(user => user.ClerkId)
            .IsUnique();
    }

    public DbSet<UserModel>? Users { get; set; }
    public DbSet<SavingModel>? Savings { get; set; }
    public DbSet<SavingBalanceModel>? SavingsBalance { get; set; }
}