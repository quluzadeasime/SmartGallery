using FluentValidation;
using Smart.Business.DTOs.BrandDTOs;
using Smart.Business.Validators.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.BrandValidators
{
    public class UpdateBrandDTOValidator : BaseEntityValidator<UpdateBrandDTO>
    {
        public UpdateBrandDTOValidator()
        {
            RuleFor(x => x.Image)
                .NotEmpty()
                .WithMessage("Logo is required.");
        }
    }
}
