using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.BrandDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.BrandValidators;

namespace Smart.API.Controllers
{
    public class BrandController : BaseAPIController
    {
        protected readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _brandService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdBrandDTO { Id = id };
            var validation = await new GetByIdBrandDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _brandService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateBrandDTO dto)
        {
            var validation = await new CreateBrandDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _brandService.CreateAsync(dto))
              : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateBrandDTO dto)
        {
            var validation = await new UpdateBrandDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _brandService.UpdateAsync(dto))
              : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteBrandDTO { Id = id };
            var validation = await new DeleteBrandDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _brandService.DeleteAsync(dto))
               : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
