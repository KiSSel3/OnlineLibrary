using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.Author;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class AuthorUpdateRequestDTOValidator : AbstractValidator<AuthorUpdateRequestDTO>
{
    public AuthorUpdateRequestDTOValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("ID is required.");

        RuleFor(x => x.AuthorDTO)
            .NotNull().WithMessage("AuthorDTO is required.")
            .SetValidator(new AuthorDTOValidator());
    }
}