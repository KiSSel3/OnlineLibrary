using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class UserRoleProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserRoleDTO, UserRoleEntity>();
        config.NewConfig<UserRoleEntity, UserRoleDTO>();
    }
}
