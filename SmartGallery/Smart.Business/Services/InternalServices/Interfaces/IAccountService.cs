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
        Task<ConfirmEmailResponseDTO> PasswordConfirmationAsync(ConfirmEmailDTO dto);
        Task EmailConfirmationAsync(ConfirmEmailDTO dto);
        Task ClickToResendAsync(ClickToResendDTO dto);

        //Login methods
        Task<LoginResponseDTO> LoginAsync(LoginUserDTO dto);
        Task ResetPasswordAsync(ResetPasswordUserDTO dto);
        Task<ForgotPasswordResponseDTO> ForgotPasswordAsync(ForgotPasswordDTO dto);

        // Account Methods
        Task<ChangePasswordResponseDTO> ChangePasswordAsync(ChangePasswordUserDTO dto, string userId);
        Task<UpdateUserResponseDTO> UpdateAsync(UpdateUserDTO dto, string userId);
    }
}
