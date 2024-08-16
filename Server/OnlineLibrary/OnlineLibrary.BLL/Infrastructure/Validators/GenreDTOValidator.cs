using FluentValidation;
using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class GenreDTOValidator : AbstractValidator<GenreDTO>
{
    public GenreDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}