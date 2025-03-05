using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.SpecificationDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.SpecificationValidators;

namespace Smart.API.Controllers
{
    public class SpecificationController : BaseAPIController
    {
        protected readonly ISpecificationService _specificationService;

        public SpecificationController(ISpecificationService specificationService)
        {
            _specificationService = specificationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _specificationService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdSpecificationDTO { Id = id };
            var validation = await new GetByIdSpecificationDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _specificationService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateSpecificationDTO dto)
        {
            var validation = await new CreateSpecificationDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _specificationService.CreateAsync(dto))
              : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateSpecificationDTO dto)
        {
            var validation = await new UpdateSpecificationDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _specificationService.UpdateAsync(dto))
              : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteSpecificationDTO { Id = id };
            var validation = await new DeleteSpecificationDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _specificationService.DeleteAsync(dto))
               : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
