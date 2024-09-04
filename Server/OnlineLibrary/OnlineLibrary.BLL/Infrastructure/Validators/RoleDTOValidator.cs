using FluentValidation;
using OnlineLibrary.BLL.DTOs.Common;
using OnlineLibrary.BLL.DTOs.Responses.Role;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class RoleDTOValidator : AbstractValidator<RoleResponseDTO>
{
    public RoleDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(50).WithMessage("Name cannot exceed 50 characters.");
    }
}