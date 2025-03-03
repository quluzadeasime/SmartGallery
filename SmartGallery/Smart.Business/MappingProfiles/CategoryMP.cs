using AutoMapper;
using Smart.Business.DTOs.CategoryDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class CategoryMP : Profile
    {
        public CategoryMP()
        {
            //Create section
            CreateMap<CreateCategoryDTO, Category>().ReverseMap();

            //Update section
            CreateMap<UpdateCategoryDTO, Category>().ReverseMap();
        }
    }
}
