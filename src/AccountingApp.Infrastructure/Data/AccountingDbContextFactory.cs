using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AccountingApp.Infrastructure.Data;

public class AccountingDbContextFactory : IDesignTimeDbContextFactory<AccountingDbContext>
{
    public AccountingDbContext CreateDbContext(string[] args)
    {
        var connection = "Host=127.0.0.1;Port=5432;Database=AccountingDb;User Id=postgres";
        var optionsBuilder = new DbContextOptionsBuilder<AccountingDbContext>();

        optionsBuilder.UseNpgsql(connection);

        return new AccountingDbContext(optionsBuilder.Options);
    }
}