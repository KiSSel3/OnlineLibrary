using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;

namespace OnlineLibrary.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "AdminArea")]
[Area("admin")]
[Route("/api/book")]
public class BookController : Controller
{
    private readonly ICreateNewBookUseCase _createNewBookUseCase;
    private readonly IDeleteBookUseCase _deleteBookUseCase;
    private readonly IUpdateBookUseCase _updateBookUseCase;

    public BookController(ICreateNewBookUseCase createNewBookUseCase, IDeleteBookUseCase deleteBookUseCase, IUpdateBookUseCase updateBookUseCase)
    {
        _createNewBookUseCase = createNewBookUseCase;
        _deleteBookUseCase = deleteBookUseCase;
        _updateBookUseCase = updateBookUseCase;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromForm] BookCreateRequestDTO bookCreateRequestDTO, CancellationToken cancellationToken = default)
    {
        await _createNewBookUseCase.ExecuteAsync(bookCreateRequestDTO, cancellationToken);
        return Ok();
    }

    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateAsync([FromForm] BookUpdateRequestDTO bookUpdateRequestDTO, CancellationToken cancellationToken = default)
    {
        await _updateBookUseCase.ExecuteAsync(bookUpdateRequestDTO,cancellationToken);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _deleteBookUseCase.ExecuteAsync(id, cancellationToken);
        return Ok();
    }
}