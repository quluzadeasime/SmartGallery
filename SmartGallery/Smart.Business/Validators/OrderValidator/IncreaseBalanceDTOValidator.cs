using FluentValidation;
using Smart.Business.DTOs.TransactionDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.OrderValidator
{
    public class IncreaseBalanceDTOValidator : AbstractValidator<IncreaseBalanceDTO>
    {
        public IncreaseBalanceDTOValidator()
        {
            RuleFor(x => x.UserId)
               .NotEmpty()
               .WithMessage("Please enter username or email address.");

            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Please enter your password.")
                .GreaterThan(4)
                .WithMessage("Minimum amount must be 5 azn.");
        }
    }
}
