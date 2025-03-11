using Microsoft.AspNetCore.Http;
using Smart.Business.DTOs.SpecificationDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.DTOs.ProductDTOs
{
        public class CreateProductDTO
        {
            public string Name { get; set; }
            public int BrandId { get; set; }
            public float Rating { get; set; }
            public decimal Price { get; set; }
            public int? Discount { get; set; }
            public int CategoryId { get; set; }
            public int RatingCount { get; set; }
            public string Description { get; set; }
            public ICollection<int>? ColorIds { get; set; }
            public ICollection<IFormFile> Images { get; set; } 
    }
}
