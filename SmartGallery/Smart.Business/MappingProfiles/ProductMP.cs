using AutoMapper;
using Smart.Business.DTOs.ColorDTOs;
using Smart.Business.DTOs.ProductDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class ProductMP : Profile
    {
        public ProductMP()
        {
            // Create section
            CreateMap<CreateProductDTO, Product>().ReverseMap();

            // Update section
            CreateMap<CreateProductDTO, Product>().ReverseMap();
        }
    }
}

