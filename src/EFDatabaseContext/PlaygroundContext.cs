using EFDatabaseContext.Models;
using Microsoft.EntityFrameworkCore;

namespace EFDatabaseContext;

public sealed class PlaygroundContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public PlaygroundContext(DbContextOptions options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.Migrate();
        // Database.EnsureCreated();
    }
}