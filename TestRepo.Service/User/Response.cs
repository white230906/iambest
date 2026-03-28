namespace TestRepo.Service.User;

public class Response
{
    public static class Massage
    {
        public static string Created = "Created";
        public static string Updated = "Updated";
        public static string Deleted = "Deleted";
    }

    public class UserResponse
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}