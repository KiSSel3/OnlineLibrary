using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Author;

public class GetAllAuthorsUseCase : IGetAllAuthorsUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllAuthorsUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AuthorResponseDTO>> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        var authors = await _unitOfWork.GetBaseRepository<AuthorEntity>().GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<AuthorResponseDTO>>(authors);
    }
}