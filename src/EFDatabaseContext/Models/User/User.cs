namespace EFDatabaseContext.Models.User;

using Company;
using UserProfile;

public class User: Entity
{
    public string Login { get; set; }
    
    public string Password { get; set; }
    public Guid CompanyId { get; set; }
    public Company Company { get; set; }
    public UserProfile Profile { get; set; }
   
}