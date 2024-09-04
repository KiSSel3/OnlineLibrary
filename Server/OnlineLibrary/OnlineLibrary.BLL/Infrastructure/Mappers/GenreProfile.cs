using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Genre;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class GenreProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GenreResponseDTO, GenreEntity>();
        config.NewConfig<GenreEntity, GenreResponseDTO>();
    }
}
