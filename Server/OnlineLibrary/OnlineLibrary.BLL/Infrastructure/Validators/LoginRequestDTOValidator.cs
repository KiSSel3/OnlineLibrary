using FluentValidation;
using OnlineLibrary.BLL.DTOs.Request.User;

namespace OnlineLibrary.BLL.Infrastructure.Validators;

public class LoginRequestDTOValidator : AbstractValidator<LoginRequestDTO>
{
    public LoginRequestDTOValidator()
    {
        RuleFor(x => x.Login)
            .NotEmpty().WithMessage("Login is required.")
            .Length(3, 50).WithMessage("Login must be between 3 and 50 characters.")
            .Matches(@"^[a-zA-Z0-9]+$").WithMessage("Login must contain only alphanumeric characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");
    }
}