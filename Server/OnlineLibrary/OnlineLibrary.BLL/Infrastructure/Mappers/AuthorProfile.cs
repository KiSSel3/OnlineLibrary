using Mapster;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Request.Author;
using OnlineLibrary.BLL.DTOs.Responses.Author;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class AuthorProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthorEntity, AuthorResponseDTO>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.AuthorDTO, src => new AuthorDTO
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                DateOfBirth = src.DateOfBirth,
                Country = src.Country
            });

        config.NewConfig<AuthorUpdateRequestDTO, AuthorEntity>()
            .Map(dest => dest.Id, src => src.Id)
            .Map(dest => dest.FirstName, src => src.AuthorDTO.FirstName)
            .Map(dest => dest.LastName, src => src.AuthorDTO.LastName)
            .Map(dest => dest.DateOfBirth, src => src.AuthorDTO.DateOfBirth)
            .Map(dest => dest.Country, src => src.AuthorDTO.Country);

        config.NewConfig<AuthorDTO, AuthorEntity>();
        config.NewConfig<AuthorEntity, AuthorDTO>();
    }
}