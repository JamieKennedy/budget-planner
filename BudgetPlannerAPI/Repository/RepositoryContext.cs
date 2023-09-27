using Common.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public RepositoryContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Income>().HasOne(i => i.User).WithMany(u => u.Incomes).OnDelete(DeleteBehavior.NoAction);
    }


    public DbSet<Token>? Tokens { get; set; }
    public DbSet<Savings>? Savings { get; set; }
    public DbSet<SavingsBalance>? SavingsBalance { get; set; }
    public DbSet<Contributor>? Contributors { get; set; }
    public DbSet<ExpenseCategory>? ExpenseCategories { get; set; }
    public DbSet<RecurringExpenses>? RecurringExpenses { get; set; }
    public DbSet<Account>? Accounts { get; set; }
    public DbSet<Income>? Income { get; set; }
}