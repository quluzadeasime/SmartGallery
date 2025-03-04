using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.ServiceDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.ServiceValidators;

namespace Smart.API.Controllers
{
    public class ServiceController : BaseAPIController
    {
        protected readonly IServiceService _serviceService;

        public ServiceController(IServiceService serviceService)
        {
            _serviceService = serviceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _serviceService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdServiceDTO { Id = id };
            var validation = await new GetByIdServiceDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _serviceService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateServiceDTO dto)
        {
            var validation = await new CreateServiceDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _serviceService.CreateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromForm] UpdateServiceDTO dto)
        {
            var validation = await new UpdateServiceDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _serviceService.UpdateAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteServiceDTO() { Id = id };
            var validation = await new DeleteServiceDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _serviceService.DeleteAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
