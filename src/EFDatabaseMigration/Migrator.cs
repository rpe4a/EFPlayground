using EFDatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace EFDatabaseMigration;

public class Migrator:IDesignTimeDbContextFactory<PlaygroundContext>
{
    public PlaygroundContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var dbContextBuilder = new DbContextOptionsBuilder<PlaygroundContext>();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        dbContextBuilder.UseSqlite(connectionString, o => o.MigrationsAssembly("EFDatabaseMigration"));

        return new PlaygroundContext(dbContextBuilder.Options);
    }
}