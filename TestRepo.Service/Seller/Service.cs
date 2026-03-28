using Microsoft.EntityFrameworkCore;
using TestRepo.Repository;

namespace TestRepo.Service.Seller;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateSeller(Request.SellerRequest request)
    {
        var emailQuery = _dbContext.Sellers.Where(x => x.User.Email == request.Email);
        var existEmail = await emailQuery.AnyAsync();
        if (existEmail)
        {
            throw new Exception("Email already exists");
        }

        var newUser = new Repository.Entity.User()
        {
            Email = request.Email,
            Password = request.Password,
            Role = "Seller"
        };
        _dbContext.Add(newUser);
        await _dbContext.SaveChangesAsync();
        var newSeller = new Repository.Entity.Seller()
        {
            UserId = newUser.Id,
            CompanyAddress = request.CompanyAddress,
            CompanyName = request.CompanyName,
            TaxCode =  request.TaxCode
        };
        _dbContext.Add(newSeller);
        await _dbContext.SaveChangesAsync();
        return Response.Massage.Created;
        
    }
}