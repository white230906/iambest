namespace TestRepo.Service.Base;

public class Response
{
    public class PageResult<T>
    {
        public List<T> Items { get; set; } = new List<T>();
        public int totalItems { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}