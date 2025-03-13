using App.DAL.Presistence;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Smart.Business.DTOs.UserDTOs;
using Smart.Business.Services.ExternalServices.Interfaces;
using Smart.Business.Services.InternalServices.Interfaces;
using Smart.Core.Entities.Identity;
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
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _http;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, AppDbContext dbContext, IConfiguration configuration, IMapper mapper, IHttpContextAccessor http)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            _configuration = configuration;
            _mapper = mapper;
            _http = http;
        }

        public Task<ChangePasswordResponseDTO> ChangePasswordAsync(ChangePasswordUserDTO dto, string userId)
        {
            throw new NotImplementedException();

        }

        public Task ChangeUserStatus(string userId)
        {
            throw new NotImplementedException();
        }

        public void CheckIdentityResult(IdentityResult result)
        {
            throw new NotImplementedException();
        }

        public Task<User> CheckNotFoundByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<User> CheckNotFoundForLoginByUsernameOrEmailAsync(string userNameOrEmail)
        {
            throw new NotImplementedException();
        }

        public Task CheckUserPasswordAsync(User user, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckUserStatus(string userNameOrEmail)
        {
            throw new NotImplementedException();
        }

        public Task ClickToResendAsync(ClickToResetDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPasswordAsync(ForgotPasswordDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserRoleAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDTO> LoginAsync(LoginUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task LogoutAsync(LogoutDTO dto)
        {
            //var oldUser = await CheckNotFoundByIdAsync(dto.UserId);

            
        }

        public Task<RegisterUserResponseDTO> RegisterAsync(RegisterUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task ResetPasswordAsync(ResetPasswordUserDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<UpdateUserResponseDTO> Update(UpdateUserDTO dto, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
