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
            // Create section: CreateProductDTO -> Product
            CreateMap<CreateProductDTO, Product>().ReverseMap();

            // Update section: CreateProductDTO -> Product (Aynı şəkildə təkrarlamaq lazım ola bilər)
            CreateMap<CreateProductDTO, Product>().ReverseMap();
        }
    }
}

