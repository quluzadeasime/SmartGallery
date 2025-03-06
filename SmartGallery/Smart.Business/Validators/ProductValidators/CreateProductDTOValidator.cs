using FluentValidation;
using Smart.Business.DTOs.ProductDTOs;
using Smart.Business.DTOs.SpecificationDTOs;
using Smart.Business.Services.ExternalServices.Abstractions;
using Smart.Business.Validators.SpecificationValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.ProductValidators
{
    public class CreateProductDTOValidator : AbstractValidator<CreateProductDTO>
    {
        public CreateProductDTOValidator()
        {
            RuleFor(x => x.Name)
           .NotEmpty()
           .WithMessage("Product name is required.")
           .Length(3, 100)
           .WithMessage("Product name must be between 3 and 100 characters.");

            RuleFor(x => x.BrandId)
                .GreaterThan(0)
                .WithMessage("BrandId must be greater than 0.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Discount)
                .InclusiveBetween(0, 100)
                .When(x => x.Discount.HasValue)
                .WithMessage("Discount must be between 0 and 100.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0)
                .WithMessage("CategoryId must be greater than 0.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required.")
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ColorIds)
                .NotEmpty()
                .WithMessage("At least one color must be selected.");

            RuleFor(x => x.Images)
                .NotEmpty()
                .WithMessage("At least one image is required.")
                .Must(images => images.All(img => img != null && FileManagerService.BeAValidImage(img)))
                .WithMessage("All files must be valid images.");

            //RuleForEach(x => x.Specifications)
            //    .SetValidator(new CreateSpecificationDTOValidator());
        }
    
    }
}
