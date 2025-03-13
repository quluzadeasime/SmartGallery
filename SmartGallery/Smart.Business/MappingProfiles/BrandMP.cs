using App.Business.MappingProfiles.Commons;
using AutoMapper;
using Smart.Business.DTOs.BrandDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class BrandMP : Profile
    {
        public BrandMP()
        {
            //Create section
            CreateMap<CreateBrandDTO, Brand>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .AfterMap<CustomMappingAction<CreateBrandDTO, Brand>>()
                .ReverseMap();

            //Update section
            CreateMap<UpdateBrandDTO, Brand>()
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .AfterMap<CustomMappingAction<UpdateBrandDTO, Brand>>()
                .ReverseMap();
        }
    }
}
