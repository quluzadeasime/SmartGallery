using AutoMapper;
using Smart.Business.DTOs.SpecificationDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class SpecificationMP : Profile
    {
        public SpecificationMP()
        {
            //Create section
            CreateMap<CreateSpecificationDTO, Specification>().ReverseMap();

            //Update section
            CreateMap<UpdateSpecificationDTO, Specification>().ReverseMap();
        }
    }
}
