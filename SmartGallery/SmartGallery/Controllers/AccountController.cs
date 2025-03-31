
using AppTech.Business.Validators.UserValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Smart.API.Controllers.Commons;
using Smart.Business.DTOs.UserDTOs;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Business.Validators.UserValidators;
using System.Security.Claims;

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
        [AllowAnonymous]
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

            return validations.IsValid ? Ok(await _accountService.RegisterAsync(dto)) :
                BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPost("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailDTO dto)
        {
            var validations = await new ConfirmEmailDTOValidator().ValidateAsync(dto);

            if (!validations.IsValid)
                BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });

            await _accountService.EmailConfirmationAsync(dto);

            return Ok();
        }

        [HttpPost("confirm-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmPasswordAsync([FromBody] ConfirmEmailDTO dto)
        {
            var validations = await new ConfirmEmailDTOValidator().ValidateAsync(dto);
            if (!validations.IsValid)
                return BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });

            var response = await _accountService.PasswordConfirmationAsync(dto);

            return Ok(response);
        }


        [HttpPut("update")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateUserDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
                return Unauthorized("User ID not found in token.");

            var validations = await new UpdateUserDTOValidator().ValidateAsync(dto);
            if (!validations.IsValid)
                return BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });

            await _accountService.UpdateAsync(dto, userId);

            return Ok(new { message = "User data has been successfully updated." });
        }


        [HttpPost("click-to-resend")]
        [AllowAnonymous]
        public async Task<IActionResult> ClickToResendAsync([FromBody] ClickToResendDTO dto)
        {
            var validations = await new ClickToResendDTOValidator().ValidateAsync(dto);
            await _accountService.ClickToResendAsync(dto);

            return validations.IsValid ? Ok() : BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPost("forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordDTO dto)
        {
            var validations = await new ForgotPasswordDTOValidator().ValidateAsync(dto);
            await _accountService.ForgotPasswordAsync(dto);

            return validations.IsValid ? Ok() :
                BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPost("reset-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordUserDTO dto)
        {
            var validations = await new ResetPasswordUserDTOValidator().ValidateAsync(dto);
            await _accountService.ResetPasswordAsync(dto);

            return validations.IsValid ? Ok() :
                BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
        }

        [HttpPost("change-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordUserDTO dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
                return Unauthorized("User ID not found in token.");

            var validations = await new ChangePasswordUserDTOValidator().ValidateAsync(dto);
            await _accountService.ChangePasswordAsync(dto, userId);

            return validations.IsValid ? Ok() :
                BadRequest(new { Errors = validations.Errors.Select(e => e.ErrorMessage).ToList() });
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
