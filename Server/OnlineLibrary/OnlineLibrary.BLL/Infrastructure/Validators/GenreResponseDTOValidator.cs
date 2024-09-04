using FluentValidation;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Genre;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class GenreResponseDTOValidator : AbstractValidator<GenreResponseDTO>
{
    public GenreResponseDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
    }
}