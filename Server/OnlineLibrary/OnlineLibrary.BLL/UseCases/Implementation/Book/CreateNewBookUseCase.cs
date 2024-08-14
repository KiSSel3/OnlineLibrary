using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Book;

public class CreateNewBookUseCase : ICreateNewBookUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateNewBookUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(BookCreateRequestDTO bookRequestDTO, CancellationToken cancellationToken = default)
    {
        var newBook = _mapper.Map<BookEntity>(bookRequestDTO);

        if (bookRequestDTO.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await bookRequestDTO.Image.CopyToAsync(memoryStream, cancellationToken);
                newBook.Image = memoryStream.ToArray();
            }
        }

        await _unitOfWork.GetCustomRepository<IBookRepository>().CreateAsync(newBook, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}