using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.DTOs.Request.Book;
using OnlineLibrary.BLL.DTOs.Request.Loan;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.DTOs.Responses.Book;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public static class MapsterConfig
{
    public static void Configure()
    {
        //Author
        TypeAdapterConfig<AuthorEntity, AuthorResponseDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.AuthorDTO, src => new AuthorDTO
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                Country = src.Country
            });
        
        TypeAdapterConfig<AuthorUpdateRequestDTO, AuthorEntity>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.AuthorDTO.FirstName)
            .Map(dest => dest.LastName, src => src.AuthorDTO.LastName)
            .Map(dest => dest.DateOfBirth, src => src.AuthorDTO.DateOfBirth)
            .Map(dest => dest.Country, src => src.AuthorDTO.Country);

        TypeAdapterConfig<AuthorDTO, AuthorEntity>.NewConfig();
        TypeAdapterConfig<AuthorEntity, AuthorDTO>.NewConfig();
        
        //Book
        TypeAdapterConfig<BookCreateRequestDTO, BookEntity>.NewConfig()
            .Map(dest => dest.ISBN, src => src.BookDTO.ISBN)
            .Map(dest => dest.Title, src => src.BookDTO.Title)
            .Map(dest => dest.Description, src => src.BookDTO.Description)
            .Map(dest => dest.GenreId, src => src.GenreId)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Ignore(dest => dest.Image);

        TypeAdapterConfig<BookUpdateRequestDTO, BookEntity>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.ISBN, src => src.BookDTO.ISBN)
            .Map(dest => dest.Title, src => src.BookDTO.Title)
            .Map(dest => dest.Description, src => src.BookDTO.Description)
            .Map(dest => dest.GenreId, src => src.GenreId)
            .Map(dest => dest.AuthorId, src => src.AuthorId)
            .Ignore(dest => dest.Image);
        
        TypeAdapterConfig<BookEntity, BookResponseDTO>.NewConfig()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.BookDTO, src => new BookDTO
            {
                ISBN = src.ISBN,
                Title = src.Title,
                Description = src.Description
            })
            .Map(dest => dest.Image, src => src.Image);
        
        TypeAdapterConfig<BookDTO, BookEntity>.NewConfig();
        TypeAdapterConfig<BookEntity, BookDTO>.NewConfig();
        
        //Genre
        TypeAdapterConfig<GenreDTO, GenreEntity>.NewConfig();
        TypeAdapterConfig<GenreEntity, GenreDTO>.NewConfig();
        
        //Loan
        TypeAdapterConfig<LoanCreateRequestDTO, LoanEntity>.NewConfig();
        
        //Role
        TypeAdapterConfig<RoleDTO, RoleEntity>.NewConfig();
        TypeAdapterConfig<RoleEntity, RoleDTO>.NewConfig();

        //UserRole
        TypeAdapterConfig<UserRoleDTO, UserRoleEntity>.NewConfig();
        TypeAdapterConfig<UserRoleEntity, UserRoleDTO>.NewConfig();
        
        //User
        TypeAdapterConfig<UserRequestDTO, UserEntity>.NewConfig();
        TypeAdapterConfig<UserEntity, UserResponseDTO>.NewConfig();
    }
}