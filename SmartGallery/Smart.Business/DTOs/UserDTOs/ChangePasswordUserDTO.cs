using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.UserDTOs
{
    public class ChangePasswordUserDTO
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }

    public class ChangePasswordResponseDTO
    {
        public string Message { get; set; }
    }
}
