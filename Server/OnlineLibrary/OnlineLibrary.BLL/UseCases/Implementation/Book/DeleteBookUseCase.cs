using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Book;

public class DeleteBookUseCase : IDeleteBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteBookUseCase(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        var bookRepository = _unitOfWork.GetCustomRepository<IBookRepository>();

        var book = await bookRepository.GetByIdAsync(bookId, cancellationToken);
        if (book == null)
        {
            throw new EntityNotFoundException("Book", bookId);
        }
        
        bookRepository.Delete(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}