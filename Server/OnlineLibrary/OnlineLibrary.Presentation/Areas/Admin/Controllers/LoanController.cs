using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;

namespace OnlineLibrary.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "AdminArea")]
[Area("admin")]
[Route("/api/loan")]
public class LoanController : Controller
{
    private readonly IDeleteLoanByBookIdUseCase _deleteLoanByBookIdUseCase;

    public LoanController(IDeleteLoanByBookIdUseCase deleteLoanByBookIdUseCase)
    {
        _deleteLoanByBookIdUseCase = deleteLoanByBookIdUseCase;
    }
    
    [HttpDelete("delete/{bookId}")]
    public async Task<IActionResult> DeleteAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        await _deleteLoanByBookIdUseCase.ExecuteAsync(bookId, cancellationToken);
        return Ok();
    }
}