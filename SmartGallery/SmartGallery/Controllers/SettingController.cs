using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.SettingDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.SettingValidators;

namespace Smart.API.Controllers
{
    public class SettingController : BaseAPIController
    {
        protected readonly ISettingService _settingService;

        public SettingController(ISettingService settingService)
        {
            _settingService = settingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _settingService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdSettingDTO { Id = id };
            var validation = await new GetByIDSettingDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _settingService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateSettingDTO dto)
        {
            var validation = await new CreateSettingDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _settingService.CreateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateSettingDTO dto)
        {
            var validation = await new UpdateSettingDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _settingService.UpdateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteSettingDTO() { Id = id };
            var validation = await new DeleteSettingDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _settingService.DeleteAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
