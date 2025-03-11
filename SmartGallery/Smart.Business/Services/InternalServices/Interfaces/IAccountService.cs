using Microsoft.AspNetCore.Identity;
using Smart.Business.DTOs.UserDTOs;
using Smart.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.Services.InternalServices.Interfaces
{
    public interface IAccountService
    {
        //Register methods
        Task<RegisterUserResponseDTO> RegisterAsync(RegisterUserDTO dto);
        Task ClickToResendAsync(ClickToResetDTO dto);

        //Login methods
        Task CheckUserPasswordAsync(User user, string password);
        Task<LoginResponseDTO> LoginAsync(LoginUserDTO dto);
        Task ResetPasswordAsync(ResetPasswordUserDTO dto);
        Task ForgotPasswordAsync(ForgotPasswordDTO dto);
        Task LogoutAsync(LogoutDTO dto);

        // Account Methods
        Task<ChangePasswordResponseDTO> ChangePasswordAsync(ChangePasswordUserDTO dto, string userId);
        Task<UpdateUserResponseDTO> Update(UpdateUserDTO dto, string userId);

        Task<User> CheckNotFoundForLoginByUsernameOrEmailAsync(string userNameOrEmail);
        Task<bool> CheckUserStatus(string userNameOrEmail);
        Task<User> CheckNotFoundByIdAsync(string userId);
        void CheckIdentityResult(IdentityResult result);
        Task<string> GetUserRoleAsync(User user);
        Task ChangeUserStatus(string userId);
    }
}
