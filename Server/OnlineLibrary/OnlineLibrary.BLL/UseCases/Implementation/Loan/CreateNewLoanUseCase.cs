using MapsterMapper;
using OnlineLibrary.BLL.DTOs.Request.Loan;
using OnlineLibrary.BLL.UseCases.Interfaces.Loan;
using OnlineLibrary.DAL.Infrastructure.Interfaces;
using OnlineLibrary.DAL.Repositories.Interfaces;
using OnlineLibrary.Domain.Entities;

namespace OnlineLibrary.BLL.UseCases.Implementation.Loan;

public class CreateNewLoanUseCase : ICreateNewLoanUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateNewLoanUseCase(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task ExecuteAsync(LoanCreateRequestDTO loanRequestDTO, CancellationToken cancellationToken)
    {
        var loanRepository = _unitOfWork.GetCustomRepository<ILoanRepository>();
        
        var isBookAvailable = await loanRepository.IsBookAvailableAsync(loanRequestDTO.BookId, cancellationToken);
        if (!isBookAvailable)
        {
            throw new InvalidOperationException("The book is currently unavailable for loan.");
        }

        var loan = _mapper.Map<LoanEntity>(loanRequestDTO);
        loan.BorrowedAt = DateTime.UtcNow;
        loan.ReturnBy = DateTime.UtcNow.AddDays(loanRequestDTO.DayCount);
        
        await loanRepository.CreateAsync(loan, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}