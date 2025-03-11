using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.BrandDTOs
{
    public class CreateBrandDTO
    {
        public IFormFile ImageUrl { get; set; }
    }
}
