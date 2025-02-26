using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class PostBranchMapping:Profile
    {
        public PostBranchMapping()
        {
            CreateMap<BranchPostModel, Branch>();
        }
    }
}
