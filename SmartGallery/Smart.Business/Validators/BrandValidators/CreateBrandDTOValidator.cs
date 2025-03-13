using FluentValidation;
using Smart.Business.DTOs.BrandDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.BrandValidators
{
    public class CreateBrandDTOValidator : AbstractValidator<CreateBrandDTO>
    {
        public CreateBrandDTOValidator()
        {
            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage("Logo is required!");
        }
    }
}
