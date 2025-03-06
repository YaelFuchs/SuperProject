using AutoMapper;
using Super.Core.Models;
using SuperAPI.Models;

namespace SuperAPI.Mapping
{
    public class ShoppingCartMapping : Profile
    {
        public ShoppingCartMapping()
        {
            CreateMap<ShoppingCartModel, ShoppingCartItem>();
        }
    }
}
