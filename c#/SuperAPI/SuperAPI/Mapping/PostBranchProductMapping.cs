using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class PostBranchProductMapping:Profile
    {
        public PostBranchProductMapping()
        {
            CreateMap<BranchProductPostModel, BranchProduct>();
        }
    }
}
