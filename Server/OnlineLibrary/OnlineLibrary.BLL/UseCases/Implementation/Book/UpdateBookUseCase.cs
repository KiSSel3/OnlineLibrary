using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Book;

public class UpdateBookUseCase : IUpdateBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(BookUpdateRequestDTO bookRequestDTO, CancellationToken cancellationToken = default)
    {
        var bookRepository = _unitOfWork.GetCustomRepository<IBookRepository>();

        var book = await bookRepository.GetByIdAsync(bookRequestDTO.Id, cancellationToken);
        if (book == null)
        {
            throw new EntityNotFoundException("Book", bookRequestDTO.Id);
        }

        book = _mapper.Map<BookEntity>(bookRequestDTO);
        
        if (bookRequestDTO.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await bookRequestDTO.Image.CopyToAsync(memoryStream, cancellationToken);
                book.Image = memoryStream.ToArray();
            }
        }
        
        bookRepository.Update(book);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}