namespace TestRepo.Service.User;

public interface IService
{
    public Task<string> CreateUser(Request.UserRequest userRequest);
    public Task<Base.Response.PageResult<Response.UserResponse>> GetSellers(string? searchTerm, int PageIndex, int PageSize);
}