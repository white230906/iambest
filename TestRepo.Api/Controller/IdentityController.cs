using Microsoft.AspNetCore.Mvc;
using TestRepo.Service.IdentityService;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class IdentityController: ControllerBase
{
    private readonly IService _identityService;
    public  IdentityController(IService identityService)
    {
        _identityService = identityService;
    }

    [HttpPost]
    public async Task<IActionResult> Login(Request.Login loginRequest)
    {
        var  token = await _identityService.Login(loginRequest);
        return Ok(token);
    }
}