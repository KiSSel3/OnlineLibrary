using MapsterMapper;
using Microsoft.Extensions.Caching.Memory;
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
    private readonly IMemoryCache _cache;
    private readonly IMapper _mapper;

    public UpdateBookUseCase(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task ExecuteAsync(BookUpdateRequestDTO bookRequestDTO, CancellationToken cancellationToken = default)
    {
        var bookRepository = _unitOfWork.GetCustomRepository<IBookRepository>();

        var existingBook = await bookRepository.GetByIdAsync(bookRequestDTO.Id, cancellationToken);
        if (existingBook == null)
        {
            throw new EntityNotFoundException("Book", bookRequestDTO.Id);
        }

        existingBook.Title = bookRequestDTO.BookDTO.Title;
        existingBook.Description = bookRequestDTO.BookDTO.Description;
        existingBook.GenreId = bookRequestDTO.GenreId;
        existingBook.AuthorId = bookRequestDTO.AuthorId;
        
        if (bookRequestDTO.Image != null)
        {
            using (var memoryStream = new MemoryStream())
            {
                await bookRequestDTO.Image.CopyToAsync(memoryStream, cancellationToken);
                existingBook.Image = memoryStream.ToArray();
            }
        }
        
        bookRepository.Update(existingBook);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        var cacheKey = $"BookDetails_{bookRequestDTO.Id}";

        if (_cache.TryGetValue(cacheKey, out _))
        {
            _cache.Remove(cacheKey);
        }
    }
}