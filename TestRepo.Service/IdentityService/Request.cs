namespace TestRepo.Service.IdentityService;

public class Request
{
    public class Login
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}