using FluentValidation;
using iText.Commons.Bouncycastle.Security;
using Smart.Business.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.UserValidators
{
    public class LoginUserDTOValidator : AbstractValidator<LoginUserDTO>
    {
        public LoginUserDTOValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
               .NotEmpty()
               .WithMessage("Please enter email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Please enter your password.");
        }
    }
}
