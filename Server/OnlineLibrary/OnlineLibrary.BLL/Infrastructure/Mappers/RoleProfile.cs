using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class RoleProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RoleDTO, RoleEntity>();
        config.NewConfig<RoleEntity, RoleDTO>();
    }
}
