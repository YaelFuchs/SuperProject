using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderInputModel, Order>();
        }
    }
}