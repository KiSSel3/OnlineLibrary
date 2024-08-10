using Mapster;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public static class MapsterConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<UserRequestDTO, UserEntity>.NewConfig();
        TypeAdapterConfig<UserEntity, UserResponseDTO>.NewConfig();
    }
}