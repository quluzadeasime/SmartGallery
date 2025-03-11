using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.UserDTOs
{
    public class UpdateUserDTO
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserResponseDTO
    {
        public string Message { get; set; }
    }
}
