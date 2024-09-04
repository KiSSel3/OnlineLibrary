using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
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
    
    public async Task ExecuteAsync(Guid authorId, AuthorDTO authorDTO, CancellationToken cancellationToken = default)
    {
        var authorRepository = _unitOfWork.GetBaseRepository<AuthorEntity>();

        var author = await authorRepository.GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new EntityNotFoundException("Author", authorId);
        }

        _mapper.Map(authorDTO, author);
        
        authorRepository.Update(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}