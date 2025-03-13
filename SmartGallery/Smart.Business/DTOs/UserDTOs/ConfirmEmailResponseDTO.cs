using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.UserDTOs
{
    public class ConfirmEmailResponseDTO
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
