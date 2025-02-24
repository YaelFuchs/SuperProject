using AutoMapper;
using Super.Core.DTOs;
using Super.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Super.Core.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
