using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<UserRequestDTO, UserEntity>.NewConfig();
        TypeAdapterConfig<UserEntity, UserResponseDTO>.NewConfig();

        //TypeAdapterConfig<AuthorCreateRequestDTO, AuthorEntity>.NewConfig();
        TypeAdapterConfig<AuthorUpdateRequestDTO, AuthorEntity>.NewConfig();
        TypeAdapterConfig<AuthorEntity, AuthorResponseDTO>.NewConfig();

        TypeAdapterConfig<GenreDTO, GenreEntity>.NewConfig();
        TypeAdapterConfig<GenreEntity, GenreDTO>.NewConfig();

        TypeAdapterConfig<RoleDTO, RoleEntity>.NewConfig();
        TypeAdapterConfig<RoleEntity, RoleDTO>.NewConfig();

        TypeAdapterConfig<UserRoleDTO, UserRoleEntity>.NewConfig();
        TypeAdapterConfig<UserRoleEntity, UserRoleDTO>.NewConfig();
    }
}