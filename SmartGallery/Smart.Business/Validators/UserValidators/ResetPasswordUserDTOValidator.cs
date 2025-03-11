using FluentValidation;
using Smart.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.UserValidators
{
    public class ResetPasswordUserDTOValidator : AbstractValidator<ResetPasswordUserDTO>
    {
        public ResetPasswordUserDTOValidator()
        {
            RuleFor(x => x.NewPassword)
               .NotEmpty().WithMessage("Password is required.")
               .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
               .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
               .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
               .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit.");

            RuleFor(x => x.ConfirmNewPassword)
                .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");

            RuleFor(x => x.Email)
               .NotEmpty()
               .WithMessage("Email address is required.")
               .EmailAddress()
               .WithMessage("Invalid email address.");

            RuleFor(x => x.Token)
               .NotEmpty()
               .WithMessage("Token is required.");
        }
    }
}
