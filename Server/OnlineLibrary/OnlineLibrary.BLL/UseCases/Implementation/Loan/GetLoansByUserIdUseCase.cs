using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Loan;
using OnlineLibrary.BLL.DTOs.Responses.User;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;

namespace OnlineLibrary.BLL.UseCases.Implementation.Loan;

public class GetLoansByUserIdUseCase : IGetLoansByUserIdUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetLoansByUserIdUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LoanResponseDTO>> ExecuteAsync(Guid userId, CancellationToken cancellationToken)
    {
        var loans = await _unitOfWork.GetCustomRepository<ILoanRepository>()
            .GetByUserIdAsync(userId, cancellationToken);

        var loanResponseList = new List<LoanResponseDTO>();

        foreach (var loan in loans)
        {
            var user = await _unitOfWork.GetCustomRepository<IUserRepository>()
                .GetByIdAsync(loan.UserId, cancellationToken);

            var book = await _unitOfWork.GetCustomRepository<IBookRepository>()
                .GetByIdAsync(loan.BookId, cancellationToken);

            loanResponseList.Add(new LoanResponseDTO
            {
                UserDTO = _mapper.Map<UserResponseDTO>(user),
                BookDTO = _mapper.Map<BookDTO>(book),
                BorrowedAt = loan.BorrowedAt,
                ReturnBy = loan.ReturnBy
            });
        }

        return loanResponseList;
    }
}