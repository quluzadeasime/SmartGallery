using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.SettingDTOs
{
    public class CreateSettingDTO
    {
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string LogoUrl { get; set; }
        public string Instagram { get; set; }
        public string Facebook { get; set; }
        public string WorkHours { get; set; }
    }
}
