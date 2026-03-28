namespace TestRepo.Service.IdentityService;

public interface IService
{
    public Task<Response.IdentityResponse> Login(Request.Login login);
}