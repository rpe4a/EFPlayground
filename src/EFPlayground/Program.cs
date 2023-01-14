using EFDatabaseContext;
using EFDatabaseContext.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EFPlayground
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            var configuration = GetConfiguration();
            var options = GetDbContextOptions(configuration);

            await using PlaygroundContext db = new PlaygroundContext(options);
            
            User tom = new User { Name = "Tom", Age = 33 };
            User alice = new User { Name = "Alice", Age = 26 };
 
            db.Users.AddRange(tom, alice);
            await db.SaveChangesAsync();
 
            await ShowUsers(db);

            alice.Age = 35;
            db.Users.Update(alice);
            await db.SaveChangesAsync();

            await ShowUsers(db);

            db.Users.Remove(alice);
            await db.SaveChangesAsync();

            await ShowUsers(db);
        }

        private static DbContextOptions<PlaygroundContext> GetDbContextOptions(IConfigurationRoot configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlaygroundContext>();
         
            return optionsBuilder
                .UseSqlite(configuration.GetConnectionString("DefaultConnection"), o => o.MigrationsAssembly("EFDatabaseMigration"))
                //.LogTo(Console.WriteLine)
                .Options;
        }

        private static IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            return builder.Build();
        }

        private static async Task ShowUsers(PlaygroundContext db)
        {
            var users = await db.Users.ToListAsync();
            foreach (User user in users)
            {
                Console.WriteLine($"{user.Id}.{user.Name} - {user.Age}");
            }
        }
    }
}