using FluentValidation;
using Smart.Business.DTOs.SpecificationDTOs;
using Smart.Business.Validators.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.SpecificationValidators
{
    public class UpdateSpecificationDTOValidator : BaseEntityValidator<UpdateSpecificationDTO>
    {
        public UpdateSpecificationDTOValidator()
        {
            RuleFor(x => x.Key)
               .NotEmpty()
               .WithMessage("Key is required")
               .Length(3, 50)
               .WithMessage("The key must be between 3 and 50 characters. ");

            RuleFor(x => x.Value)
                .NotEmpty()
                .WithMessage("Value is required")
                .Length(3, 150)
                .WithMessage("The value must be between 3 and 150 characters. ");
        }
    }
}
