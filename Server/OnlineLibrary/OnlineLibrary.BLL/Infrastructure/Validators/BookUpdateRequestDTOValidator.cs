using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.Book;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class BookUpdateRequestDTOValidator : AbstractValidator<BookUpdateRequestDTO>
{
    public BookUpdateRequestDTOValidator()
    {
        RuleFor(x => x.ISBN)
            .NotEmpty().WithMessage("ISBN is required.")
            .Length(10, 13).WithMessage("ISBN must be between 10 and 13 characters.");

        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(100).WithMessage("Title cannot exceed 100 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("Genre ID is required.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author ID is required.");
    }
}