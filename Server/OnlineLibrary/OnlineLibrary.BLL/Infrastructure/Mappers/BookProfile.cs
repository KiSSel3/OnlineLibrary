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
            .Ignore(dest => dest.Image);

        config.NewConfig<BookUpdateRequestDTO, BookEntity>()
            .Ignore(dest => dest.Image);

        config.NewConfig<BookEntity, BookResponseDTO>()
            .Map(dest => dest.Image, src => src.Image != null && src.Image.Length > 0 
                ? Convert.ToBase64String(src.Image) 
                : string.Empty);

        config.NewConfig<BookEntity, BookDetailsResponseDTO>()
            .Map(dest => dest.Image, src => src.Image != null && src.Image.Length > 0 
                ? Convert.ToBase64String(src.Image) 
                : string.Empty);
        
        config.NewConfig<BookDTO, BookEntity>();
        config.NewConfig<BookEntity, BookDTO>();
    }
}
