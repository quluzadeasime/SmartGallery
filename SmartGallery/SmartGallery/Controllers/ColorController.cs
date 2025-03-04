using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.ColorDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.ColorValidators;

namespace Smart.API.Controllers
{
    public class ColorController : BaseAPIController
    {
        protected readonly IColorService _colorService;

        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _colorService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdColorDTO { Id = id };
            var validation = await new GetByIdColorDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _colorService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateColorDTO dto)
        {
            var validation = await new CreateColorDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _colorService.CreateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateColorDTO dto)
        {
            var validation = await new UpdateColorDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _colorService.UpdateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteColorDTO() { Id = id };
            var validation = await new DeleteColorDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _colorService.DeleteAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
