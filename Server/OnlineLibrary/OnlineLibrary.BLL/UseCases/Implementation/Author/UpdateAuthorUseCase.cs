using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Author;

public class UpdateAuthorUseCase : IUpdateAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateAuthorUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(AuthorUpdateRequestDTO authorRequestDTO, CancellationToken cancellationToken = default)
    {
        var authorRepository = _unitOfWork.GetBaseRepository<AuthorEntity>();

        var author = await authorRepository.GetByIdAsync(authorRequestDTO.Id, cancellationToken);
        if (author == null)
        {
            throw new EntityNotFoundException("Author", authorRequestDTO.Id);
        }

        author = _mapper.Map<AuthorEntity>(authorRequestDTO);
        
        authorRepository.Update(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}