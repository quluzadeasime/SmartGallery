using Smart.Business.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.WorkHourDTOs
{
    public class WorkHourDTO : BaseEntityDTO
    {
        public sbyte WeekDay { get; set; }
        public DateTime StartHour { get; set; }
        public DateTime EndHour { get; set; }
    }
}
