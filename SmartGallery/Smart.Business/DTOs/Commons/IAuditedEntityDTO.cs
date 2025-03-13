using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.Commons
{
    public interface IAuditedentityDTO
    {
        public IFormFile Image { get; set; }
    }
}
