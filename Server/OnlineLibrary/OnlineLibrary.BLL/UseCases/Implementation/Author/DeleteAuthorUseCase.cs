using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Author;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Author;

public class DeleteAuthorUseCase : IDeleteAuthorUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteAuthorUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid authorId, CancellationToken cancellationToken = default)
    {
        var authorRepository = _unitOfWork.GetBaseRepository<AuthorEntity>();

        var author = await authorRepository.GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new EntityNotFoundException("Author", authorId);
        }
        
        authorRepository.Delete(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}