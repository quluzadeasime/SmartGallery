﻿using FluentValidation;
using Smart.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.UserValidators
{
    public class ClickToResetDTOValidator : AbstractValidator<ClickToResetDTO>
    {
        public ClickToResetDTOValidator()
        {
            RuleFor(vm => vm.Email)
               .NotEmpty()
               .WithMessage("Email address is required.")
               .EmailAddress()
               .WithMessage("Invalid email address.");
        }
    }
}
