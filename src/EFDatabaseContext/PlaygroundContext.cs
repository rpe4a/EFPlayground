using EFDatabaseContext.Models.Company;
using EFDatabaseContext.Models.User;
using EFDatabaseContext.Models.UserProfile;
using Microsoft.EntityFrameworkCore;

namespace EFDatabaseContext;

public sealed class PlaygroundContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Company> Companies => Set<Company>();
    public DbSet<UserProfile> Profiles => Set<UserProfile>();

    public PlaygroundContext(DbContextOptions options) : base(options)
    {
        // Database.EnsureDeleted();
        Database.Migrate();
        // Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CompanyConfiguration());
        modelBuilder.ApplyConfiguration(new UserProfileConfiguration());
    }
}