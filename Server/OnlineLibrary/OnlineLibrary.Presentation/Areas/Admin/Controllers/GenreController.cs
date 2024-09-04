using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;

namespace OnlineLibrary.Presentation.Areas.Admin.Controllers;

[ApiController]
[Authorize(Policy = "AdminArea")]
[Area("admin")]
[Route("/api/genre")]
public class GenreController : Controller
{
    private readonly ICreateNewGenreUseCase _createNewGenreUseCase;
    private readonly IDeleteGenreUseCase _deleteGenreUseCase;
    private readonly IUpdateGenreUseCase _updateGenreUseCase;

    public GenreController(ICreateNewGenreUseCase createNewGenreUseCase, IDeleteGenreUseCase deleteGenreUseCase, IUpdateGenreUseCase updateGenreUseCase)
    {
        _createNewGenreUseCase = createNewGenreUseCase;
        _deleteGenreUseCase = deleteGenreUseCase;
        _updateGenreUseCase = updateGenreUseCase;
    }
    
    [HttpPost("create/{name}")]
    public async Task<IActionResult> CreateAsync(string name, CancellationToken cancellationToken = default)
    {
        await _createNewGenreUseCase.ExecuteAsync(name, cancellationToken);
        return Ok();
    }
    
    [HttpPut("update/{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] string name, CancellationToken cancellationToken = default)
    {
        await _updateGenreUseCase.ExecuteAsync(id, name, cancellationToken);
        return Ok();
    }
    
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        await _deleteGenreUseCase.ExecuteAsync(id, cancellationToken);
        return Ok();
    }
}