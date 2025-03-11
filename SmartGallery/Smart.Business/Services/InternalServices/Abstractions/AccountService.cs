using App.DAL.Presistence;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Smart.Business.DTOs.UserDTOs;
using Smart.Business.Helpers;
using Smart.Business.Services.ExternalServices.Abstractions;
using Smart.Business.Services.ExternalServices.Interfaces;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities.Identity;
using Smart.Core.Exceptions.AccountExceptions;
using Smart.Core.Exceptions.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Abstractions
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly ILanguageService _languageService;
        private readonly SignInManager<User> _signInManager;
        private readonly AccountErrorMessages _errorMessages;

        public AccountService(UserManager<User> userManager, IConfiguration configuration,
            IMapper mapper, IHttpContextAccessor http, SignInManager<User> signInManager,
            ILanguageService languageService, AccountErrorMessages errorMessages, IEmailService emailService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
            _http = http;
            _signInManager = signInManager;
            _languageService = languageService;
            _errorMessages = errorMessages;
            _emailService = emailService;
        }


        // MAIN METHODS
        public async Task<RegisterUserResponseDTO> RegisterAsync(RegisterUserDTO dto)
        {
            var newUser = _mapper.Map<User>(dto);
            var number = GenerateConfirmationNumber();

            newUser.ConfirmationCode = number;
            newUser.ConfirmationCodeSentAt = DateTime.UtcNow;

            CheckIdentityResult(await _userManager.CreateAsync(newUser, dto.Password));
            CheckIdentityResult(await _userManager.AddToRoleAsync(newUser, "Student"));

            await _emailService.SendMailMessageAsync(newUser.Email, newUser, newUser.ConfirmationCode.Value, string.Empty);

            return new RegisterUserResponseDTO
            {
                Email = dto.Email,
            };
        }

        public async Task<UpdateUserResponseDTO> UpdateAsync(UpdateUserDTO dto, string userId)
        {
            var oldUser = await CheckNotFoundByIdAsync(userId);

            CheckIdentityResult(await _userManager.UpdateAsync(_mapper.Map(dto, oldUser)));

            return new UpdateUserResponseDTO
            {
                Message = "User data has been succesfully updated."
            };
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginUserDTO dto)
        {
            var user = await CheckNotFoundForLoginByUsernameOrEmailAsync(dto.UsernameOrEmail);
            var userRole = await GetUserRoleAsync(user);

            await CheckUserPasswordAsync(user, dto.Password);

            var token = JWTGenerator.GenerateToken(user, userRole, _configuration);

            return new LoginResponseDTO
            {
                Token = token,
            };
        }



        public async Task<ChangePasswordResponseDTO> ChangePasswordAsync(ChangePasswordUserDTO dto, string userId)
        {
            var oldUser = await CheckNotFoundByIdAsync(userId);

            if (oldUser.PasswordHash is not null)
                CheckIdentityResult(await _userManager.ChangePasswordAsync(oldUser, dto.CurrentPassword, dto.NewPassword));
            else
                CheckIdentityResult(await _userManager.AddPasswordAsync(oldUser, dto.NewPassword));

            return new ChangePasswordResponseDTO
            {
                Message = "Your password has been successfully changed.",
            };
        }

        public Task ResetPasswordAsync(ResetPasswordUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPasswordAsync(ForgotPasswordDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task ClickToResendAsync(ClickToResendDTO dto)
        {
            var oldUser = await _userManager.FindByEmailAsync(dto.Email);
            CheckConfirmationCodeSendAt(oldUser);

            var number = GenerateConfirmationNumber();

            oldUser.IsResent = true;
            oldUser.ConfirmationCode = number;
            oldUser.ConfirmationCodeSentAt = DateTime.UtcNow;

            CheckIdentityResult(await _userManager.UpdateAsync(oldUser));

            await _emailService.SendMailMessageAsync(oldUser.Email, oldUser, oldUser.ConfirmationCode.Value, string.Empty);
        }

        public async Task EmailConfirmationAsync(ConfirmEmailDTO dto)
        {
            var oldUser = await _userManager.FindByEmailAsync(dto.Email);
            CheckConfirmationNumber(oldUser.ConfirmationCode, dto.Number);

            oldUser.EmailConfirmed = true;
            oldUser.ConfirmationCode = null;
            oldUser.ConfirmationCodeSentAt = null;
            oldUser.IsResent = false;

            CheckIdentityResult(await _userManager.UpdateAsync(oldUser));
        }

        // SUPPORTIVE METHODS
        private int GenerateConfirmationNumber()
        {
            Random random = new Random();
            var digits = Enumerable.Range(0, 10).OrderBy(x => random.Next()).Take(6).ToArray();

            while (digits[0] == 0)
            {
                digits = Enumerable.Range(0, 10).OrderBy(x => random.Next()).Take(6).ToArray();
            }

            return int.Parse(string.Join("", digits));
        }

        private async Task<string> GetUserRoleAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        }

        private void CheckConfirmationNumber(int? userConfirmationNumber, int number)
        {
            if (userConfirmationNumber != number)
                throw new ConfirmationNumberIsNotValidException(_errorMessages.ConfirmationNumberIsNotValid);
        }

        private void CheckConfirmationCodeSendAt(User oldUser)
        {
            if (oldUser.ConfirmationCodeSentAt.HasValue &&
                (DateTime.UtcNow - oldUser.ConfirmationCodeSentAt.Value).TotalMinutes < 1
                && oldUser.IsResent)
                throw new ConfirmationNumberIsNotValidException(_errorMessages.ConfirmationNumberIsNotValid);
        }

        private void CheckIdentityResult(IdentityResult result)
        {
            if (result.Errors.Any(e => e.Code == "TokenExpired"))
                throw new UserTokenExpiredException(_errorMessages.UserTokenExpired);

            if (!result.Succeeded)
                throw new UserIdentityResultException($"{result.Errors.FirstOrDefault()?.Description}");
        }

        private async Task CheckUserPasswordAsync(User user, string password)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

            if (!user.EmailConfirmed)
                throw new EmailIsNotConfirmedException(_errorMessages.EmailIsNotConfirmed);

            if (result.IsLockedOut)
                throw new UserLockOutException(_errorMessages.UserLockOut);

            if (!result.Succeeded)
                throw new UsernameOrEmailAddressNotFoundException(_errorMessages.UsernameOrEmailAddressNotFound);
        }

        private async Task<User> CheckNotFoundByIdAsync(string userId)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(x => x.Id == userId) ??
                throw new UsernameOrEmailAddressNotFoundException(_errorMessages.UsernameOrEmailAddressNotFound);
        }

        private async Task<User> CheckNotFoundForLoginByUsernameOrEmailAsync(string userNameOrEmail)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(x => x.UserName.ToLower() == userNameOrEmail.ToLower()
                                        || x.Email.ToLower() == userNameOrEmail.ToLower());

            return user ?? throw new UsernameOrEmailAddressNotFoundException(_errorMessages.UsernameOrEmailAddressNotFound);
        }

        private async Task<User> CheckNotFoundForForgotPasswordByEmailAsync(string userEmail)
        {
            return await _userManager.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower() == userEmail.ToLower()) ??
                throw new UsernameOrEmailAddressNotFoundException(_errorMessages.UsernameOrEmailAddressNotFound);
        }

    }
}
