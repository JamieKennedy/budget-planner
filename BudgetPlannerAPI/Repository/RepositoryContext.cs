﻿using Common.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public RepositoryContext(DbContextOptions options) : base(options) { }



    public DbSet<Token>? Tokens { get; set; }
    public DbSet<Savings>? Savings { get; set; }
    public DbSet<SavingsBalance>? SavingsBalance { get; set; }
    public DbSet<Group>? Groups { get; set; }
    public DbSet<GroupMember> GroupMembers { get; set; }
}