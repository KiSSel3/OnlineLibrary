using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;

namespace OnlineLibrary.Presentation.Controllers;

[ApiController]
[Route("/api/genre")]
public class GenreController : Controller
{
    private readonly IGetAllGenresUseCase _getAllGenresUseCase;
    private readonly IGetGenreByIdUseCase _getGenreByIdUseCase;

    public GenreController(IGetAllGenresUseCase getAllGenresUseCase, IGetGenreByIdUseCase getGenreByIdUseCase)
    {
        _getAllGenresUseCase = getAllGenresUseCase;
        _getGenreByIdUseCase = getGenreByIdUseCase;
    }
    
    [HttpGet("get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var genres = await _getAllGenresUseCase.ExecuteAsync(cancellationToken);
        return Ok(genres);
    }

    [HttpGet("get-by-id/{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var genre = await _getGenreByIdUseCase.ExecuteAsync(id, cancellationToken);
        return Ok(genre);
    }
}