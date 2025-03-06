using Microsoft.AspNetCore.Http;
using Smart.Business.DTOs.Commons;
using Smart.Business.DTOs.SpecificationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.ProductDTOs
{
    public class UpdateProductDTO : BaseEntityDTO
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public decimal Price { get; set; }
        public int? Discount { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public ICollection<int> ColorIds { get; set; }
        public ICollection<IFormFile> ImageUrls { get; set; }
        public ICollection<UpdateSpecificationDTO> Specifications { get; set; }
    }
}
