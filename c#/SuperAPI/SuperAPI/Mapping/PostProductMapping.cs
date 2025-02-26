using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class PostProductMapping:Profile
    {
        public PostProductMapping()
        {
            CreateMap<ProductPostModel, Product>();
        }
    }
}
