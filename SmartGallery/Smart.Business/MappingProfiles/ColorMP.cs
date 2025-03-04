using AutoMapper;
using Smart.Business.DTOs.ColorDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class ColorMP : Profile
    {
        public ColorMP()
        {
            //Create section
            CreateMap<CreateColorDTO, Color>().ReverseMap();

            //Update section
            CreateMap<UpdateColorDTO, Color>().ReverseMap();
        }
    }
}
