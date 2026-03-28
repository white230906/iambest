using Microsoft.EntityFrameworkCore;
using TestRepo.Repository;

namespace TestRepo.Service.User;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateUser(Request.UserRequest userRequest)
    {
        var emailQuery = _dbContext.Users.Where(u => u.Email == userRequest.Email);
        var existEmail = await emailQuery.AnyAsync();
        if (existEmail)
        { 
            throw new Exception("Email already exists");
        }

        var newUser = new Repository.Entity.User()
        {
            Email = userRequest.Email,
            Password = userRequest.Password,
            Role = "User"
        };
        _dbContext.Add(newUser);
        await _dbContext.SaveChangesAsync();
        return Response.Massage.Created;
    }

    public async Task<Base.Response.PageResult<Response.UserResponse>> GetSellers(string? searchTerm, int PageIndex, int PageSize)
    {
        var query = _dbContext.Users.Where(u => true);
        if (searchTerm != null)
        {
            query = query.Where(u => u.Email.Contains(searchTerm));
        }
        query = query.OrderBy(u => u.Email);
        query = query.Skip((PageIndex - 1) * PageSize).Take(PageSize);
        var selectedQuery = query.Select(u => new Response.UserResponse()
        {
            Email = u.Email,
            Password = u.Password,
            Role = u.Role
        });
        var resultPage = await selectedQuery.ToListAsync();
        var totalCount =  resultPage.Count;
        var result = new Base.Response.PageResult<Response.UserResponse>()
        {
            Items = resultPage,
            totalItems =  totalCount,
            PageIndex = PageIndex,
            PageSize = PageSize
            
        };
        return result;
    }
}