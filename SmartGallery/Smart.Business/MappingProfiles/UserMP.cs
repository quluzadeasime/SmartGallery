using AutoMapper;
using Smart.Business.DTOs.UserDTOs;
using Smart.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class UserMP : Profile
    {
        public UserMP()
        {
            //Create section
            CreateMap<RegisterUserDTO, User>().ReverseMap();

            //Updste section
            CreateMap<UpdateUserDTO, User>().ReverseMap();
        }
    }
}
