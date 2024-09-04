using MapsterMapper;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Genre;

public class UpdateGenreUseCase : IUpdateGenreUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateGenreUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(Guid genreId, string genreName, CancellationToken cancellationToken = default)
    {
        var genreRepository = _unitOfWork.GetBaseRepository<GenreEntity>();

        var genre = await genreRepository.GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", genreId);
        }

        genre.Name = genreName;
        
        genreRepository.Update(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}