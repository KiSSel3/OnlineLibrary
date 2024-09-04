using MapsterMapper;
using Microsoft.Extensions.Caching.Memory;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.DTOs.Responses.Genre;
using OnlineLibrary.BLL.Exceptions;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Book;

public class GetBookByIdUseCase : IGetBookByIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IMemoryCache _cache;
    public GetBookByIdUseCase(IUnitOfWork unitOfWork, IMapper mapper, IMemoryCache cache)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<BookDetailsResponseDTO> ExecuteAsync(Guid bookId, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"BookDetails_{bookId}";
        
        if (!_cache.TryGetValue(cacheKey, out BookDetailsResponseDTO cachedBookDetails))
        {
            var book = await GetBookByIdAsync(bookId, cancellationToken);
            var genre = await GetGenreByIdAsync(book.GenreId, cancellationToken);
            var author = await GetAuthorByIdAsync(book.AuthorId, cancellationToken);
            
            cachedBookDetails = new BookDetailsResponseDTO
            {
                AuthorResponseDTO = _mapper.Map<AuthorResponseDTO>(author),
                BookResponseDTO = _mapper.Map<BookResponseDTO>(book),
                GenreDTO = _mapper.Map<GenreResponseDTO>(genre),
            };

            if (book.Image != null)
            {
                cachedBookDetails.Image = Convert.ToBase64String(book.Image);
            }
            
            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));
            
            _cache.Set(cacheKey, cachedBookDetails, cacheEntryOptions);
        }
        
        return cachedBookDetails;
    }

    private async Task<BookEntity> GetBookByIdAsync(Guid bookId, CancellationToken cancellationToken)
    {
        var book = await _unitOfWork.GetCustomRepository<IBookRepository>().GetByIdAsync(bookId, cancellationToken);
        if (book == null)
        {
            throw new EntityNotFoundException("Book", bookId);
        }

        return book;
    }

    private async Task<GenreEntity> GetGenreByIdAsync(Guid genreId, CancellationToken cancellationToken)
    {
        var genre = await _unitOfWork.GetBaseRepository<GenreEntity>().GetByIdAsync(genreId, cancellationToken);
        if (genre == null)
        {
            throw new EntityNotFoundException("Genre", genreId);
        }

        return genre;
    }

    private async Task<AuthorEntity> GetAuthorByIdAsync(Guid authorId, CancellationToken cancellationToken)
    {
        var author = await _unitOfWork.GetBaseRepository<AuthorEntity>().GetByIdAsync(authorId, cancellationToken);
        if (author == null)
        {
            throw new EntityNotFoundException("Author", authorId);
        }

        return author;
    }
}