using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Smart.Business.DTOs.ProductDTOs;
using Smart.Business.Services.ExternalServices.Abstractions;
using Smart.Business.Validators.Commons;
using Smart.Business.Validators.SpecificationValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Validators.ProductValidators
{
    public class UpdateProductDTOValidator : BaseEntityValidator<UpdateProductDTO>
    {
        public UpdateProductDTOValidator()
        {
            RuleFor(x => x.Id)
           .GreaterThan(0)
           .WithMessage("Product Id must be greater than 0.");

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

            //RuleFor(x => x.ImageUrls)
            //   .NotEmpty()
            //   .WithMessage("At least one image URL is required.")
            //   .Must(x => x.All(url => FileManagerService.upl())) 
            //   .WithMessage("All image URLs must be valid.")
            //   .Must(images => images.All(url => FileManagerService.IsFromCloudService(url)))
            //   .WithMessage("All image URLs must come from the Cloud service.");

            RuleForEach(x => x.Specifications)
            .SetValidator(new UpdateSpecificationDTOValidator());
        }
    }
}
