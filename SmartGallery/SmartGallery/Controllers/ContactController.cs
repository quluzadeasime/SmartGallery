using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.ContactDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.ContactValidators;

namespace Smart.API.Controllers
{
    public class ContactController : BaseAPIController
    {
        protected readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _contactService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdContactDTO { Id = id };
            var validation = await new GetByIdContactDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _contactService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateContactDTO dto)
        {
            var validation = await new CreateContactDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _contactService.CreateAsync(dto))
              : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteContactDTO { Id = id };
            var validation = await new DeleteContactDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _contactService.DeleteAsync(dto))
               : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}
