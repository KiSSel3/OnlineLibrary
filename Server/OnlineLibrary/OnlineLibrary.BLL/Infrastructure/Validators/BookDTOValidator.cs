using FluentValidation;
using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class BookDTOValidator : AbstractValidator<BookDTO>
{
    public BookDTOValidator()
    {
        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Length(10, 13).WithMessage("ISBN must be between 10 and 13 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
    }
}