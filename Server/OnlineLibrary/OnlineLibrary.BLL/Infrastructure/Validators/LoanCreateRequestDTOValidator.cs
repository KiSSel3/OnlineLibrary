using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.Loan;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class LoanCreateRequestDTOValidator : AbstractValidator<LoanCreateRequestDTO>
{
    public LoanCreateRequestDTOValidator()
    {
        RuleFor(x => x.BookId)
            .NotEmpty().WithMessage("Book ID is required.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.DayCount)
            .GreaterThan(0).WithMessage("Day count must be greater than 0.");
    }
}