using Mapster;
using OnlineLibrary.BLL.DTOs.Request.User;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoginRequestDTO, UserEntity>();
        config.NewConfig<RegisterRequestDTO, UserEntity>();
        config.NewConfig<UserEntity, UserResponseDTO>();
    }
}
