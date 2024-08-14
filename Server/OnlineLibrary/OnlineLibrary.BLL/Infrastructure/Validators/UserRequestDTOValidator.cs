using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.User;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class UserRequestDTOValidator : AbstractValidator<UserRequestDTO>
{
    public UserRequestDTOValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.")
            .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Login must contain only alphanumeric characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
        
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(255).WithMessage("Email cannot exceed 255 characters.");
    }
}