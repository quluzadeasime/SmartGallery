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
        public List<int> ColorIds { get; set; }
        public List<string> ImageUrls { get; set; }
        public List<UpdateSpecificationDTO> Specifications { get; set; }
    }
}
