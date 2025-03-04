using AutoMapper;
using Smart.Business.DTOs.ServiceDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class ServiceMP : Profile
    {
        public ServiceMP()
        {
            //Create section
            CreateMap<CreateServiceDTO, Service>().ReverseMap();

            //Update section
            CreateMap<UpdateServiceDTO, Service>().ReverseMap();
        }
    }
}
