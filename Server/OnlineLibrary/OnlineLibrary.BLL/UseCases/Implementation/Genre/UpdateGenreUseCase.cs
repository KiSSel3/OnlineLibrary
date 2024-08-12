using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
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

    public async Task ExecuteAsync(GenreDTO genreDto, CancellationToken cancellationToken = default)
    {
        var genreRepository = _unitOfWork.GetBaseRepository<GenreEntity>();

        var genre = await genreRepository.GetByIdAsync(genreDto.Id, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", genreDto.Id);
        }

        genre = _mapper.Map<GenreEntity>(genreDto);
        
        genreRepository.Update(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}