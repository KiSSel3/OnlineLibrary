using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Genre;

public class GetGenreByIdUseCase : IGetGenreByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetGenreByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<GenreDTO> ExecuteAsync(Guid genreId, CancellationToken cancellationToken = default)
    {
        var genre = await _unitOfWork.GetBaseRepository<GenreEntity>().GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", genreId);
        }

        return _mapper.Map<GenreDTO>(genre);
    }
}