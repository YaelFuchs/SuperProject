
using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class PostModelsMappingProfile :Profile 
    {
        public PostModelsMappingProfile()
        {
            CreateMap<UserPostModel, User>();
        }
    }
}
