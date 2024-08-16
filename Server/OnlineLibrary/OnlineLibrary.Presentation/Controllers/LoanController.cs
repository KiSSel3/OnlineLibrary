using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.DTOs.Request.Loan;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;

namespace OnlineLibrary.Presentation.Controllers;

[ApiController]
[Route("/api/loan")]
public class LoanController : Controller
{
    private readonly ICheckIsBookAvailableUseCase _checkIsBookAvailableUseCase;
    private readonly ICreateNewLoanUseCase _createNewLoanUseCase;
    private readonly IGetLoansByUserIdUseCase _getLoansByUserIdUseCase;

    public LoanController(ICheckIsBookAvailableUseCase checkIsBookAvailableUseCase,
        ICreateNewLoanUseCase createNewLoanUseCase, IGetLoansByUserIdUseCase getLoansByUserIdUseCase)
    {
        _checkIsBookAvailableUseCase = checkIsBookAvailableUseCase;
        _createNewLoanUseCase = createNewLoanUseCase;
        _getLoansByUserIdUseCase = getLoansByUserIdUseCase;
    }

    [HttpGet("check-availability/{bookId}")]
    public async Task<IActionResult> CheckIsBookAvailableAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var isAvailable = await _checkIsBookAvailableUseCase.ExecuteAsync(bookId, cancellationToken);
        return Ok(isAvailable);
    }

    [Authorize]
    [HttpPost("create-loan")]
    public async Task<IActionResult> CreateNewLoanAsync([FromBody] LoanCreateRequestDTO loanRequestDTO, CancellationToken cancellationToken)
    {
        await _createNewLoanUseCase.ExecuteAsync(loanRequestDTO, cancellationToken);
        return Ok();
    }

    [Authorize]
    [HttpGet("get-user-loans/{userId}")]
    public async Task<IActionResult> GetLoansByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var loans = await _getLoansByUserIdUseCase.ExecuteAsync(userId, cancellationToken);
        return Ok(loans);
    }
}