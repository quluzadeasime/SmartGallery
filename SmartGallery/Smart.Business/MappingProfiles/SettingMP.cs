using AutoMapper;
using Smart.Business.DTOs.ColorDTOs;
using Smart.Business.DTOs.SettingDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class SettingMP : Profile
    {
        public SettingMP()
        {
            //Create section
            CreateMap<CreateSettingDTO, Setting>().ReverseMap();

            //Update section
            CreateMap<UpdateSettingDTO, Setting>().ReverseMap();
        }
    }
}
