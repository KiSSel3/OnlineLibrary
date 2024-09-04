using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;

namespace OnlineLibrary.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "AdminArea")]
[Area("admin")]
[Route("/api/author")]
public class AuthorController : ControllerBase
{
    private readonly ICreateNewAuthorUseCase _createNewAuthorUseCase;
    private readonly IDeleteAuthorUseCase _deleteAuthorUseCase;
    private readonly IUpdateAuthorUseCase _updateAuthorUseCase;

    public AuthorController(ICreateNewAuthorUseCase createNewAuthorUseCase, IDeleteAuthorUseCase deleteAuthorUseCase, IUpdateAuthorUseCase updateAuthorUseCase)
    {
        _createNewAuthorUseCase = createNewAuthorUseCase;
        _deleteAuthorUseCase = deleteAuthorUseCase;
        _updateAuthorUseCase = updateAuthorUseCase;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync([FromBody] AuthorDTO authorDTO, CancellationToken cancellationToken = default)
    {
        await _createNewAuthorUseCase.ExecuteAsync(authorDTO, cancellationToken);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] AuthorDTO authorDTO, CancellationToken cancellationToken = default)
    {
        await _updateAuthorUseCase.ExecuteAsync(id, authorDTO, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _deleteAuthorUseCase.ExecuteAsync(id, cancellationToken);
        return Ok();
    }
}

