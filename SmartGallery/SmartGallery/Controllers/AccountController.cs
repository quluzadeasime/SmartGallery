using AppTech.Business.Validators.UserValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.UserDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.UserValidators;

namespace Smart.API.Controllers
{
    public class AccountController : BaseAPIController
    {
        protected readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet("check-token-authorize")]
        [Authorize]
        public IActionResult IsCustomAuthorizedAsync()
        {
            return Ok(new { message = "This is a protected method. You have authorized with custom token.", IsLogin = true });
        }

        // Registration Section
        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserDTO dto)
        {
            var validations = await new RegisterUserDTOValidator().ValidateAsync(dto);
            var registerResponse = await _accountService.RegisterAsync(dto);

            return validations.IsValid ? Ok(registerResponse) :
                BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailDTO dto)
        {
            var validations = await new ConfirmEmailDTOValidator().ValidateAsync(dto);
            await _accountService.EmailConfirmationAsync(dto);

            return validations.IsValid ? Ok() : BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPost("click-to-resend")]
        [AllowAnonymous]
        public async Task<IActionResult> ClickToResendAsync([FromBody] ClickToResendDTO dto)
        {
            var validations = await new ClickToResendDTOValidator().ValidateAsync(dto);
            await _accountService.ClickToResendAsync(dto);

            return validations.IsValid ? Ok() : BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        // Login Section
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserDTO dto)
        {
            var validations = await new LoginUserDTOValidator().ValidateAsync(dto);

            var loginResponse = await _accountService.LoginAsync(dto);

            var token = loginResponse.Token;

            if (validations.IsValid)
            {
                return Ok(new { message = "Login successful", token });
            }
            else
            {
                return BadRequest(
                    new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
            }
        }
    }
}
