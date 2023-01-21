using EFDatabaseContext;
using EFDatabaseContext.Models.Company;
using EFDatabaseContext.Models.User;
using EFDatabaseContext.Models.UserProfile;
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
            Console.WriteLine("some adds");

            Company company1 = new Company {Name = "Google"};
            Company company2 = new Company {Name = "Microsoft"};

            User user1 = new User
                {Login = "Login1", Password = "Password1", Company = company1};
            User user2 = new User
                {Login = "Login2", Password = "Password2", Company = company2};
            User user3 = new User
                {Login = "Login3", Password = "Password3", Company = company2};

            UserProfile userProfile1 = new UserProfile()
            {
                Name = "Tom", Age = 22,
                OfficialData = new OfficialData() {Passport = "5544 173389", Phone = "89125933761"}, User = user1
            };
            UserProfile userProfile2 = new UserProfile()
            {
                Name = "Ban", Age = 45,
                OfficialData = new OfficialData() {Passport = "5544 173381", Phone = "89125933762"}, User = user2
            };
            UserProfile userProfile3 = new UserProfile()
            {
                Name = "Liza", Age = 33,
                OfficialData = new OfficialData() {Passport = "5544 173382", Phone = "89125933763"}, User = user3
            };


            db.Companies.AddRange(company1, company2); // добавление компаний
            db.Users.AddRange(user1, user2, user3); // добавление пользователей
            db.Profiles.AddRange(userProfile1, userProfile2, userProfile3); // добавление профилей

            await db.SaveChangesAsync();

            await ShowUsers(db);

            Console.WriteLine("Some updates");
            await Task.Delay(3000);
            userProfile3.Name = "131231";
            userProfile3.Age = 20;

            user1.Login = "Login4";

            db.Profiles.Update(userProfile3);
            db.Users.Update(user1);

            await db.SaveChangesAsync();

            await ShowUsers(db);

            Console.WriteLine("optimistic lock example");
            var task1 = Task.Run(() => UpdateUserByLogin("Login2"));
            var task2 = Task.Run(() => UpdateUserByLogin("Login4"));
            var task3 = Task.Run(() => UpdateUserByLogin("Login2"));

            await Task.WhenAll(task1, task2, task3);
        }

        private static async Task UpdateUserByLogin(string login)
        {
            try
            {
                var configuration = GetConfiguration();
                var options = GetDbContextOptions(configuration);

                await using PlaygroundContext db = new PlaygroundContext(options);

                var user = await db.Users.FirstOrDefaultAsync(x => x.Login == login);

                user.Login = "Login" + Guid.NewGuid();

                db.Users.Update(user);
                await Task.Delay(Random.Shared.Next(10000));
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }
        }

        private static DbContextOptions<PlaygroundContext> GetDbContextOptions(IConfigurationRoot configuration)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PlaygroundContext>();

            return optionsBuilder
                .UseSqlite(
                    configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly("EFDatabaseMigration"))
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
            var users = await db.Users
                .Include(x => x.Company)
                .Include(x => x.Profile)
                .ToListAsync();
            foreach (User user in users)
            {
                Console.WriteLine($"{user.Login} - {user.Profile.Name} from company: {user.Company.Name}");
                Console.WriteLine();
                Console.WriteLine();
            }
        }
    }
}