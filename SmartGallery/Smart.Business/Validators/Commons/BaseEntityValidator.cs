using FluentValidation;
using Smart.Business.DTOs.Commons;
using Smart.Core.Entities.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.Commons
{
    public class BaseEntityValidator<T> : AbstractValidator<T> where T : BaseEntityDTO
    {
        public BaseEntityValidator()
        {
            RuleFor(entity => entity.Id)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
        }
    }
}
