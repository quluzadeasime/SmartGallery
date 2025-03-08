using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Smart.Business.DTOs.SubscriptionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.SubscriptionValidators
{
    public class CreateSubscriptionDTOValidator : AbstractValidator<CreateSubscriptionDTO>
    {
        public CreateSubscriptionDTOValidator()
        {
            RuleFor(x => x.Email)
               .NotEmpty()
               .EmailAddress()
               .WithMessage("Email is required.")
               .Length(7, 75)
               .WithMessage("Email must be between 7 and 75 characters.");
        }
    }
}
