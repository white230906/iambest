namespace TestRepo.Service.Category;

public class Response
{
    public static class Massage
    {
        public static string Created = "Created";
        public static string Updated = "Updated";
        public static string Deleted = "Deleted";
    }

    public class CategoryResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}