using FluentValidation;
using Smart.Business.DTOs.ContactDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.ContactValidators
{
    public class UpdateContactDTOValidator : AbstractValidator<UpdateContactDTO>
    {
        public UpdateContactDTOValidator()
        {
            RuleFor(x => x.Email)
               .EmailAddress()
               .NotEmpty()
               .WithMessage("Email is required.")
               .Length(7, 75)
               .WithMessage("Email must be between 7 and 75 characters.");

            RuleFor(x => x.Phone)
               .NotEmpty()
               .WithMessage("Phone number is required.")
               .Length(8, 15)
               .WithMessage("Phone number must be between 8 and 15 characters.");

            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("The fullname is required.")
               .Length(3, 60)
               .WithMessage("The fullname must be between 3 and 60 characters.");

            RuleFor(x => x.Message)
               .NotEmpty()
               .WithMessage("Message is required.")
               .Length(15, 300)
               .WithMessage("Message must be between 15 and 300 characters.");
        }
    }
}
