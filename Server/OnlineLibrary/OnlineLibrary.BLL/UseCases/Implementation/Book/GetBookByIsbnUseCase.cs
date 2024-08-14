using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Book;


//TODO: Refactoring
public class GetBookByIsbnUseCase : IGetBookByIsbnUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetBookByIsbnUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<BookDetailsResponseDTO> ExecuteAsync(string isbn, CancellationToken cancellationToken = default)
    {
        var book = await _unitOfWork.GetCustomRepository<IBookRepository>().GetByISBNAsync(isbn, cancellationToken);
        if (book == null)
        {
            throw new EntityNotFoundException($"Can't find Book with ISBN {isbn}.\"");
        }

        var genre = await _unitOfWork.GetBaseRepository<GenreEntity>().GetByIdAsync(book.GenreId, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", book.GenreId);
        }
        
        var author = await _unitOfWork.GetBaseRepository<AuthorEntity>().GetByIdAsync(book.AuthorId, cancellationToken);
        if (author == null)
        {
            throw new EntityNotFoundException("Author", book.AuthorId);
        }

        var bookDetailsDTO = new BookDetailsResponseDTO
        {
            AuthorDTO = _mapper.Map<AuthorDTO>(author),
            BookDTO = _mapper.Map<BookDTO>(book),
            GenreDTO = _mapper.Map<GenreDTO>(genre),
            Image = book.Image
        };
        
        return bookDetailsDTO;
    }
}