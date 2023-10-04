using AccountingApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AccountingApp.Infrastructure.Data;

public class AccountingDbContext : DbContext
{
    public AccountingDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Position> Positions => Set<Position>();

    public DbSet<Report> Reports => Set<Report>();

    public DbSet<Bonus> Bonuses => Set<Bonus>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}