using Microsoft.EntityFrameworkCore;
using TestRepo.Repository;

namespace TestRepo.Service.Category;

public class Service: IService
{
    private readonly AppDbContext _dbContext;
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<string> CreateCategory(Request.CategoryRequest categoryRequest)
    {
        var queryName = _dbContext.Categories.Where(x => x.Name == categoryRequest.Name);
        var existName = await queryName.AnyAsync();
        if (existName)
        {
            throw new Exception("Category already exists");
        }

        var newCategory = new Repository.Entity.Category()
        {
            Name = categoryRequest.Name,
            ParentId = categoryRequest.ParentId
        };
        _dbContext.Add(newCategory);
        await _dbContext.SaveChangesAsync();
        return Response.Massage.Created;
    }

    public async Task<List<Response.CategoryResponse>> GetCategories()
    {
        var query = _dbContext.Categories.Where(x => true);
        query = query.OrderBy(x => x.Name);
        var selectedQuery = query.Select(x => new Response.CategoryResponse()
        {
            Id = x.Id,
            Name = x.Name
        });
        var result =  await selectedQuery.ToListAsync();
        return result;

    }

    public async Task<List<Response.CategoryResponse>> GetAllCategoriesByParentId(Guid parentId)
    {
        var query = _dbContext.Categories.Where(x => x.ParentId == parentId);
        query = query.OrderBy(x => x.Name);
        var selectedQuery = query.Select(x => new Response.CategoryResponse()
        {
            Id = x.Id,
            Name = x.Name
        });
        var result =  await selectedQuery.ToListAsync();
        return result;
    }
}