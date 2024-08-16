using FluentValidation;
using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class RoleDTOValidator : AbstractValidator<RoleDTO>
{
    public RoleDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
    }
}