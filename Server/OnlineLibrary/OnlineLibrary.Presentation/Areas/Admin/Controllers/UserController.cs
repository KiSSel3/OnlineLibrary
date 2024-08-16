using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.UseCases.Interfaces.User;

namespace OnlineLibrary.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "AdminArea")]
[Area("admin")]
[Route("/api/user")]
public class UserController : Controller
{
    private readonly IGetAllUsersUseCase _getAllUsersUseCase;

    public UserController(IGetAllUsersUseCase getAllUsersUseCase)
    {
        _getAllUsersUseCase = getAllUsersUseCase;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var users = await _getAllUsersUseCase.ExecuteAsync(cancellationToken);
        return Ok(users);
    }
}