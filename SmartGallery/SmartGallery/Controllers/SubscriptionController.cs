using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.SubscriptionDTOs;
using Smart.Business.Services.InternalServices.Abstractions;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.SubscriptionValidators;

namespace Smart.API.Controllers
{
    public class SubscriptionController : BaseAPIController
    {
        protected readonly ISubscriptionService _subscriptionService;

        public SubscriptionController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(await _subscriptionService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var dto = new GetByIdSubscriptionDTO { Id = id };
            var validation = await new GetByIdSubscriptionDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _subscriptionService.GetByIdAsync(dto))
                : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateSubscriptionDTO dto)
        {
            var validation = await new CreateSubscriptionDTOValidator().ValidateAsync(dto);
            if (!validation.IsValid)
                return BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());

            var isSubscribed = await _subscriptionService.IsSubscribedAsync(dto.Email);
            if (isSubscribed)
                return BadRequest("Siz artıq abunə olmusunuz.");

            return Ok(await _subscriptionService.CreateAsync(dto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var dto = new DeleteSubscriptionDTO { Id = id };
            var validation = await new DeleteSubscriptionDTOValidator().ValidateAsync(dto);

            return validation.IsValid ? Ok(await _subscriptionService.DeleteAsync(dto))
               : BadRequest(validation.Errors.Select(x => x.ErrorMessage).ToList());
        }
    }
}

