using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Genre;

public class DeleteGenreUseCase : IDeleteGenreUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteGenreUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid genreId, CancellationToken cancellationToken = default)
    {
        var genreRepository = _unitOfWork.GetBaseRepository<GenreEntity>();

        var genre = await genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", genreId);
        }
        
        genreRepository.Delete(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}