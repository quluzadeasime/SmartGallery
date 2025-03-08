using AutoMapper;
using Smart.Business.DTOs.ContactDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class ContactMP : Profile
    {
        public ContactMP()
        {
            //Create section
            CreateMap<CreateContactDTO, Contact>().ReverseMap();
        }
    }
}
