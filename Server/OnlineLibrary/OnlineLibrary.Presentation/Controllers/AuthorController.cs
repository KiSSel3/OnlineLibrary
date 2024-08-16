using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;

namespace OnlineLibrary.Presentation.Controllers;

[ApiController]
[Route("/api/author")]
public class AuthorController : Controller
{
    private readonly IGetAllAuthorsUseCase _getAllAuthorsUseCase;
    private readonly IGetAuthorByIdUseCase _getAuthorByIdUseCase;
    
    public AuthorController(IGetAllAuthorsUseCase getAllAuthorsUseCase, IGetAuthorByIdUseCase getAuthorByIdUseCase)
    {
        _getAllAuthorsUseCase = getAllAuthorsUseCase;
        _getAuthorByIdUseCase = getAuthorByIdUseCase;
    }

    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var authors = await _getAllAuthorsUseCase.ExecuteAsync(cancellationToken);
        return Ok(authors);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var author = await _getAuthorByIdUseCase.ExecuteAsync(id, cancellationToken);
        return Ok(author);
    }
}