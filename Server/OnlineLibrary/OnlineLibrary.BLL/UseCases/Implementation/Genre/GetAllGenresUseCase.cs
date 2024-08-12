using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.UseCases.Interfaces.Genre;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Genre;

public class GetAllGenresUseCase : IGetAllGenresUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllGenresUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreDTO>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var genres = await _unitOfWork.GetBaseRepository<GenreEntity>().GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GenreDTO>>(genres);
    }
}