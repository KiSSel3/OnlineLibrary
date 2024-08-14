using FluentValidation;
using OnlineLibrary.BLL.DTOs.Common;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class UserRoleDTOValidator : AbstractValidator<UserRoleDTO>
{
    public UserRoleDTOValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("Role ID is required.");
    }
}