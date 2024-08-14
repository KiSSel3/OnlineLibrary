using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;

namespace OnlineLibrary.Presentation.Controllers;

[ApiController]
[Route("/api/book")]
public class BookController : Controller
{
    private readonly IGetAllBooksUseCase _getAllBooksUseCase;
    private readonly IGetBookByIdUseCase _getBookByIdUseCase;
    private readonly IGetBookByIsbnUseCase _getBookByIsbnUseCase;
    
    public BookController(IGetAllBooksUseCase getAllBooksUseCase, IGetBookByIdUseCase getBookByIdUseCase, IGetBookByIsbnUseCase getBookByIsbnUseCase)
    {
        _getAllBooksUseCase = getAllBooksUseCase;
        _getBookByIdUseCase = getBookByIdUseCase;
        _getBookByIsbnUseCase = getBookByIsbnUseCase;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync([FromBody] BookParametersRequestDTO bookParametersRequestDTO, CancellationToken cancellationToken = default)
    {
        var books = await _getAllBooksUseCase.ExecuteAsync(bookParametersRequestDTO, cancellationToken);
        return Ok(books);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var book = await _getBookByIdUseCase.ExecuteAsync(id, cancellationToken);
        return Ok(book);
    }
    
    [HttpGet("get-by-isbn/{isbn}")]
    public async Task<IActionResult> GetByIsbnAsync(string isbn, CancellationToken cancellationToken = default)
    {
        var author = await _getBookByIsbnUseCase.ExecuteAsync(isbn, cancellationToken);
        return Ok(author);
    }
}