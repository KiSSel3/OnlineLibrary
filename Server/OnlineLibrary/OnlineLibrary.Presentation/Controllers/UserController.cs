using Microsoft.AspNetCore.Authorization;
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
    private readonly IRevokeUseCase _revokeUseCase;

    public UserController(ILoginUseCase loginUseCase, IRegisterUseCase registerUseCase, IRefreshTokenUseCase refreshTokenUseCase, IRevokeUseCase revokeUseCase)
    {
        _loginUseCase = loginUseCase;
        _registerUseCase = registerUseCase;
        _refreshTokenUseCase = refreshTokenUseCase;
        _revokeUseCase = revokeUseCase;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequestDTO loginRequestDto, CancellationToken cancellationToken = default)
    {
        var tokenResponseDTO = await _loginUseCase.ExecuteAsync(loginRequestDto, cancellationToken);
        return Ok(tokenResponseDTO);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequestDTO registerRequestDto, CancellationToken cancellationToken = default)
    {
        var tokenResponseDTO = await _registerUseCase.ExecuteAsync(registerRequestDto, cancellationToken);
        return Ok(tokenResponseDTO);
    }
    
    [Authorize]
    [HttpDelete("logout/{userId}")]
    public async Task<IActionResult> LogoutAsync(Guid userId, CancellationToken cancellationToken)
    {
        await _revokeUseCase.ExecuteAsync(userId, cancellationToken);
        return Ok();
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] string refreshToken, CancellationToken cancellationToken)
    {
        var token = await _refreshTokenUseCase.ExecuteAsync(refreshToken, cancellationToken);
        return Ok(token);
    }
}