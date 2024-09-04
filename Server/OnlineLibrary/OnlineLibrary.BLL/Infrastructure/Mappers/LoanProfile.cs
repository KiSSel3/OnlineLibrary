using Mapster;
using OnlineLibrary.BLL.DTOs.Request.Loan;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.Infrastructure.Mappers;

public class LoanProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<LoanCreateRequestDTO, LoanEntity>();
    }
}
