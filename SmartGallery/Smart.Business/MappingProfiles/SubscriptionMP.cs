using AutoMapper;
using Smart.Business.DTOs.SubscriptionDTOs;
using Smart.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smart.Business.MappingProfiles
{
    public class SubscriptionMP : Profile
    {
        public SubscriptionMP()
        {
            //Create section
            CreateMap<CreateSubscriptionDTO, Subscription>().ReverseMap();
        }
    }
}
