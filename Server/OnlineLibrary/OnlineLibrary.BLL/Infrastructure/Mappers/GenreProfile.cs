using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class GenreProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GenreDTO, GenreEntity>();
        config.NewConfig<GenreEntity, GenreDTO>();
    }
}
