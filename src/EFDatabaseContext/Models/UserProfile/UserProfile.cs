namespace EFDatabaseContext.Models.UserProfile;

using EFDatabaseContext.Models.User;

public class UserProfile : Entity
{
    public string? Name { get; set; }
    public int Age { get; set; }
    public OfficialData OfficialData { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
}