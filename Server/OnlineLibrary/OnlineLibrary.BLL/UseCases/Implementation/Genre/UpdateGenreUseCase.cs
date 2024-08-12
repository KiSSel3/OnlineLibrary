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

    public async Task ExecuteAsync(GenreCommonDTO genreCommonDTO, CancellationToken cancellationToken = default)
    {
        var genreRepository = _unitOfWork.GetBaseRepository<GenreEntity>();

        var genre = await genreRepository.GetByIdAsync(genreCommonDTO.Id, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", genreCommonDTO.Id);
        }

        genre = _mapper.Map<GenreEntity>(genreCommonDTO);
        
        genreRepository.Update(genre);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}