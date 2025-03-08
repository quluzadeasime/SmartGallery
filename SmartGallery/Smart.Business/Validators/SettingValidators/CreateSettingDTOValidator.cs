using FluentValidation;
using Smart.Business.DTOs.SettingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.SettingValidators
{
    public class CreateSettingDTOValidator : AbstractValidator<CreateSettingDTO>
    {
        public CreateSettingDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("Email is required.")
                .Length(7, 75)
                .WithMessage("Email must be between 7 and 75 characters.");

            RuleFor(x => x.Address)
               .NotEmpty()
               .WithMessage("Address is required.")
               .Length(7, 100)
               .WithMessage("Address must be between 7 and 100 characters.");

            RuleFor(x => x.LogoUrl)
                .NotEmpty()
                .WithMessage("Logo is required.");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .WithMessage("Phone number is required.")
                .Length(8, 15)
                .WithMessage("Phone number must be between 8 and 15 characters.");

            RuleFor(x => x.Instagram)
                .NotEmpty()
                .WithMessage("Instagram link is required.");

            RuleFor(x => x.Facebook)
                .NotEmpty()
                .WithMessage("Facebook link is required.");

            RuleFor(x => x.WorkHours)
             .NotEmpty()
             .WithMessage("Work hours is required.")
             .Length(5, 200)
             .WithMessage("Work hours must be between 5 and 200 characters.");
        }
    }
}
