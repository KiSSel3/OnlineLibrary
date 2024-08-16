using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.Infrastructure.Helpers;
using OnlineLibrary.BLL.UseCases.Interfaces.Book;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Book;

public class GetAllBooksUseCase : IGetAllBooksUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllBooksUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<PagedList<BookResponseDTO>> ExecuteAsync(BookParametersRequestDTO bookParametersRequestDTO, CancellationToken cancellationToken = default)
    {
        var bookQuery = _unitOfWork.GetCustomRepository<IBookRepository>().GetBooksQueryable();

        bookQuery = ApplyFilters(bookQuery, bookParametersRequestDTO);
        
        var pagedBookList = await ApplyPaginationAsync(bookQuery, bookParametersRequestDTO, cancellationToken);

        var pagedBookDTOList = new PagedList<BookResponseDTO>(_mapper.Map<PagedList<BookResponseDTO>>(pagedBookList), pagedBookList.TotalCount,
            pagedBookList.CurrentPage, pagedBookList.PageSize);
        
        return pagedBookDTOList;
    }

    private IQueryable<BookEntity> ApplyFilters(IQueryable<BookEntity> query, BookParametersRequestDTO parameters)
    {
        if (!string.IsNullOrEmpty(parameters.SearchName))
        {
            query = query.Where(b => b.Title.Contains(parameters.SearchName));
        }

        if (parameters.GenreId.HasValue)
        {
            query = query.Where(b => b.GenreId == parameters.GenreId.Value);
        }

        if (parameters.AuthorId.HasValue)
        {
            query = query.Where(b => b.AuthorId == parameters.AuthorId.Value);
        }

        return query;
    }
    
    private async Task<PagedList<BookEntity>> ApplyPaginationAsync(IQueryable<BookEntity> bookQuery, BookParametersRequestDTO bookParametersRequestDTO, CancellationToken cancellationToken)
    {
        var pageNumber = bookParametersRequestDTO.PageNumber ?? 1;
        var pageSize = bookParametersRequestDTO.PageSize ?? await bookQuery.CountAsync(cancellationToken);

        return await PagedList<BookEntity>.ToPagedListAsync(bookQuery, pageNumber, pageSize, cancellationToken);
    }
}