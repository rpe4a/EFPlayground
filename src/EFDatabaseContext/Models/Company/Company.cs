namespace EFDatabaseContext.Models.Company;

using User;

public class Company : Entity
{
    public string Name { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
}