using Microsoft.AspNetCore.Mvc;
using TestRepo.Service.Category;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
    private readonly IService _categoryService;

    public CategoryController(IService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateCategory(Request.CategoryRequest request)
    {
        var newCategory = await _categoryService.CreateCategory(request);
        return Ok(newCategory);
    }
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var newCategory = await _categoryService.GetCategories();
        return Ok(newCategory);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var children = await _categoryService.GetAllCategoriesByParentId(id);
        return Ok(children);
    }
}