using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class AuthorProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthorEntity, AuthorResponseDTO>();
        config.NewConfig<AuthorDTO, AuthorEntity>();
        config.NewConfig<AuthorEntity, AuthorDTO>();
    }
}