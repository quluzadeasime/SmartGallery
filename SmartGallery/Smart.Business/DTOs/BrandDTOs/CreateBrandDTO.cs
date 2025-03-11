using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Http;
using Smart.Business.DTOs.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.BrandDTOs
{
    public class CreateBrandDTO : IAuditedentityDTO
    {
        public IFormFile Image { get; set; }
    }
}
