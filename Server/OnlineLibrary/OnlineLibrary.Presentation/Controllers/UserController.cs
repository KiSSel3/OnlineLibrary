using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.UseCases.Interfaces.User;

namespace OnlineLibrary.Presentation.Controllers;

[ApiController]
[Route("/api/user")]
public class UserController : Controller
{
    private readonly ILoginUseCase _loginUseCase;
    private readonly IRegisterUseCase _registerUseCase;
    private readonly IRefreshTokenUseCase _refreshTokenUseCase;

    public UserController(ILoginUseCase loginUseCase, IRegisterUseCase registerUseCase, IRefreshTokenUseCase refreshTokenUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserRequestDTO userRequestDTO, CancellationToken cancellationToken = default)
    {
        var tokenResponseDTO = await _loginUseCase.ExecuteAsync(userRequestDTO, cancellationToken);
        return Ok(tokenResponseDTO);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] UserRequestDTO userRequestDTO, CancellationToken cancellationToken = default)
    {
        var tokenResponseDTO = await _registerUseCase.ExecuteAsync(userRequestDTO, cancellationToken);
        return Ok(tokenResponseDTO);
    }
}