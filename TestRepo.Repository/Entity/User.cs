using TestRepo.Repository.Abstraction;

namespace TestRepo.Repository.Entity;

public class User: BaseEntity<Guid>, IAuditableEntity
{
    public string Password { get; set; }
    public string Email { get; set; }
    public string Role { get; set; } = "User";
    
    public Seller? Seller { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}