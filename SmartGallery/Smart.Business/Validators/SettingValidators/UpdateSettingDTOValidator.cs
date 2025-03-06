using FluentValidation;
using Smart.Business.DTOs.SettingDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.SettingValidators
{
    public class UpdateSettingDTOValidator : AbstractValidator<UpdateSettingDTO>
    {
        public UpdateSettingDTOValidator()
        {
            RuleFor(x => x.Email)
                .Must(email => !string.IsNullOrWhiteSpace(email)) 
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .Length(7, 75)
                .WithMessage("Email must be between 7 and 75 characters.");

            RuleFor(x => x.Address)
                .Length(7, 100)
                .When(x => !string.IsNullOrWhiteSpace(x.Address));

            RuleFor(x => x.LogoUrl)
                .NotEmpty()
                .WithMessage("Logo is required.");

            RuleFor(x => x.Phone)
                .Length(8, 15)
                .When(x => !string.IsNullOrWhiteSpace(x.Phone));

            RuleFor(x => x.Instagram)
                .NotEmpty()
                .WithMessage("Instagram link is required.");

            RuleFor(x => x.Facebook)
                .NotEmpty()
                .WithMessage("Facebook link is required.");

            RuleFor(x => x.WorkHours)
                .Length(5, 200)
                .When(x => !string.IsNullOrWhiteSpace(x.WorkHours));
        }
    }
}
