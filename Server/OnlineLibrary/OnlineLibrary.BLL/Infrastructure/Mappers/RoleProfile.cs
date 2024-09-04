using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Role;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class RoleProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RoleResponseDTO, RoleEntity>();
        config.NewConfig<RoleEntity, RoleResponseDTO>();
    }
}
