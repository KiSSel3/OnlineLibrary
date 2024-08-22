using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.DTOs.Request.Loan;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.BLL.Infrastructure.Helpers;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class RegisterMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //Author
        config.NewConfig<AuthorEntity, AuthorResponseDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.AuthorDTO, src => new AuthorDTO
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                Country = src.Country
            });
        
        config.NewConfig<AuthorUpdateRequestDTO, AuthorEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.AuthorDTO.FirstName)
            .Map(dest => dest.LastName, src => src.AuthorDTO.LastName)
            .Map(dest => dest.DateOfBirth, src => src.AuthorDTO.DateOfBirth)
            .Map(dest => dest.Country, src => src.AuthorDTO.Country);

        config.NewConfig<AuthorDTO, AuthorEntity>();
        config.NewConfig<AuthorEntity, AuthorDTO>();
        
        //Book
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
        
        //Genre
        config.NewConfig<GenreDTO, GenreEntity>();
        config.NewConfig<GenreEntity, GenreDTO>();
        
        //Loan
        config.NewConfig<LoanCreateRequestDTO, LoanEntity>();
        
        //Role
        config.NewConfig<RoleDTO, RoleEntity>();
        config.NewConfig<RoleEntity, RoleDTO>();

        //UserRole
        config.NewConfig<UserRoleDTO, UserRoleEntity>();
        config.NewConfig<UserRoleEntity, UserRoleDTO>();
        
        //User
        config.NewConfig<LoginRequestDTO, UserEntity>();
        config.NewConfig<RegisterRequestDTO, UserEntity>();
        config.NewConfig<UserEntity, UserResponseDTO>();
    }
}