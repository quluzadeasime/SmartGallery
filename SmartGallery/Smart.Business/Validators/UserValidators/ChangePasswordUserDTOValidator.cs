using FluentValidation;
using Smart.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.UserValidators
{
    public class ChangePasswordUserDTOValidator : AbstractValidator<ChangePasswordUserDTO>
    {
        public ChangePasswordUserDTOValidator()
        {
            RuleFor(vm => vm.CurrentPassword)
              .NotEmpty().WithMessage("Password is required.")
              .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
              .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
              .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
              .Matches("[0-9]").WithMessage("Password must contain at least one numeric digit.")
              .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one non-alphanumeric character.");

            RuleFor(x => x.NewPassword)
               .Equal(x => x.NewPassword).WithMessage("Passwords do not match.");

        }
    }
}
