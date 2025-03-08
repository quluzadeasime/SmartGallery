using FluentValidation;
using Smart.Business.DTOs.ServiceDTOs;
using Smart.Business.Validators.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.ServiceValidators
{
    public class UpdateServiceDTOValidator : BaseEntityValidator<UpdateServiceDTO>
    {
        public UpdateServiceDTOValidator()
        {
            RuleFor(x => x.Title)
              .NotEmpty()
              .WithMessage("The title is required")
              .Length(2, 50)
              .WithMessage("The title must higher than 3 and lower than 50 characters. ");

            RuleFor(x => x.Description)
               .NotEmpty()
               .WithMessage("The description is required")
               .Length(2, 150)
               .WithMessage("The description must higher than 3 and lower than 150 characters. ");

            RuleFor(x => x.Icon)
               .NotEmpty()
               .WithMessage("The icon is required");
        }
    }
}
