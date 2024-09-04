using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class BookProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<BookCreateRequestDTO, BookEntity>()
            .Map(dest => dest.ISBN, src => src.BookDTO.ISBN)
            .Map(dest => dest.Title, src => src.BookDTO.Title)
            .Map(dest => dest.Description, src => src.BookDTO.Description)
            .Map(dest => dest.GenreId, src => src.GenreId)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Ignore(dest => dest.Image);

        config.NewConfig<BookUpdateRequestDTO, BookEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ISBN, src => src.BookDTO.ISBN)
            .Map(dest => dest.Title, src => src.BookDTO.Title)
            .Map(dest => dest.Description, src => src.BookDTO.Description)
            .Map(dest => dest.GenreId, src => src.GenreId)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Ignore(dest => dest.Image);

        config.NewConfig<BookEntity, BookResponseDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.BookDTO, src => new BookDTO
            {
                ISBN = src.ISBN,
                Title = src.Title,
                Description = src.Description
            })
            .Map(dest => dest.Image, src => src.Image != null && src.Image.Length > 0 
                ? Convert.ToBase64String(src.Image) 
                : string.Empty);

        config.NewConfig<BookDTO, BookEntity>();
        config.NewConfig<BookEntity, BookDTO>();
    }
}
