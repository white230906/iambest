using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestRepo.Api.Extensions;
using TestRepo.Service.User;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private readonly IService _userService;
    public  UserController(IService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(Request.UserRequest request)
    {
        var newUser = await _userService.CreateUser(request);
        return Ok(newUser);   
    }
    
    [Authorize(Policy = JwtExtensions.AdminPolicy)]
    [HttpGet]
    public async Task<IActionResult> GetAllUsers(string? searchTerm, int PageIndex = 1, int PageSize = 10)
    {
        var newUser = await _userService.GetSellers(searchTerm, PageIndex, PageSize);
        return Ok(newUser);   
    }
    
}