using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class PostCategoryMapping:Profile
    {
        public PostCategoryMapping()
        {
            CreateMap<CategoryPostModel, Category>();
        }
    }
}
