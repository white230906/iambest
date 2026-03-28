using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestRepo.Repository;
using TestRepo.Service.JwtService;

namespace TestRepo.Service.IdentityService;

public class Service: IService
{
    private readonly JwtService.IService _jwtService;
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOptions = new JwtOptions();

    public Service(JwtService.IService jwtService, AppDbContext dbContext, IConfiguration configuration)
    {
        _jwtService = jwtService;
        _dbContext = dbContext;
        configuration.GetSection(nameof(JwtOptions)).Bind(_jwtOptions);
    }
    
    public async Task<Response.IdentityResponse> Login(Request.Login login)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(u => u.Email == login.Email);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        if (user.Password != login.Password)
        {
            throw new Exception("Wrong password");
        }

        var claims = new List<Claim>()
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("Role", user.Role),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role),
        };
        var accessToken = _jwtService.GenerateAccessToken(claims);
        return new Response.IdentityResponse()
        {
            AccessToken = accessToken,
        };

    }
}