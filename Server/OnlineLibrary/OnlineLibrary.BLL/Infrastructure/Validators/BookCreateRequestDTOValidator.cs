using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.Book;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class BookCreateRequestDTOValidator : AbstractValidator<BookCreateRequestDTO>
{
    public BookCreateRequestDTOValidator()
    {
        RuleFor(x => x.BookDTO)
            .NotNull().WithMessage("BookDTO is required.")
            .SetValidator(new BookDTOValidator());

        RuleFor(x => x.GenreId)
            .NotEmpty().WithMessage("Genre ID is required.");

        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("Author ID is required.");
    }
}