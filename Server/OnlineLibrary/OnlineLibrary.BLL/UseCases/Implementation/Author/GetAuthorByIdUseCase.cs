using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Author;

public class GetAuthorByIdUseCase : IGetAuthorByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAuthorByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<AuthorResponseDTO> ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var author = await _unitOfWork.GetBaseRepository<AuthorEntity>().GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new EntityNotFoundException("Author", authorId);
        }

        return _mapper.Map<AuthorResponseDTO>(author);
    }
}