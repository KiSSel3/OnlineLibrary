using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.Book;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class BookParametersRequestDTOValidator : AbstractValidator<BookParametersRequestDTO>
{
    public BookParametersRequestDTOValidator()
    {
        RuleFor(x => x.SearchName).MaximumLength(100)
            .WithMessage("SearchName cannot be longer than 100 characters.")
            .When(x => x.SearchName != null);

        RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1)
            .WithMessage("PageNumber must be greater than or equal to 1.")
            .When(x => x.PageNumber.HasValue);

        RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1)
            .WithMessage("PageSize must be greater than or equal to 1.")
            .When(x => x.PageSize.HasValue);
    }
}