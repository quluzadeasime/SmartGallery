using FluentValidation;
using Smart.Business.DTOs.ColorDTOs;
using Smart.Business.Validators.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.ColorValidators
{
    public class UpdateColorDTOValidator : BaseEntityValidator<UpdateColorDTO>
    {
        public UpdateColorDTOValidator()
        {
            RuleFor(x => x.Name)
               .NotEmpty()
               .WithMessage("The name is required")
               .Length(2, 50)
               .WithMessage("The name must higher than 3 and lower than 50 characters. ");
        }
    }
}
