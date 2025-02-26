using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class LoginModelsMappingProfile:Profile
    {
        public LoginModelsMappingProfile()
        {
            CreateMap<UserLoginModel, User>();
        }
    }
}
