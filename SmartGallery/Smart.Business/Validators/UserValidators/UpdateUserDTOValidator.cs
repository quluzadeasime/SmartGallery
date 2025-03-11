using FluentValidation;
using Smart.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.UserValidators
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator()
        {
            RuleFor(vm => vm.Username)
              .NotEmpty()
              .WithMessage("Last name is required.")
              .MaximumLength(50)
              .WithMessage("Last name cannot exceed 50 characters.");

            RuleFor(vm => vm.Email)
                .NotEmpty()
                .WithMessage("Email address is required.")
                .EmailAddress()
                .WithMessage("Invalid email address.");

            RuleFor(x => x.Phone)
              .NotEmpty()
              .WithMessage("Phone number is required.")
              .Length(8, 15)
              .WithMessage("Phone number must be between 8 and 15 characters.");
        }
    }
}
