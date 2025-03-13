using FluentValidation;
using Smart.Business.DTOs.UserDTOs;

namespace AppTech.Business.Validators.UserValidators
{
    public class ConfirmEmailDTOValidator : AbstractValidator<ConfirmEmailDTO>
    {
        public ConfirmEmailDTOValidator()
        {
            RuleFor(x => x.Email).Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Email address is required.")
               .EmailAddress()
               .WithMessage("Invalid email address.");

            RuleFor(x => x.Number)
               .Cascade(CascadeMode.Stop)
               .NotEmpty()
               .WithMessage("Confirmation code is required.")
               .Must(number => number >= 100000 && number <= 999999)
               .WithMessage("Confirmation code must be a 6-digit number.");
        }
    }
}