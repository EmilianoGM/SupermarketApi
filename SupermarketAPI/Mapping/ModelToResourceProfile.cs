using AutoMapper;
using SupermarketApi.WebApi.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketAPI.Extensions;
using SupermarketApi.Entities;

namespace SupermarketAPI.Mapping
{
    /*
     * When doing the mapping from one type to another, AutoMapper looks for mapping profiles.
     */
    public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<Category, CategoryResource>();
            CreateMap<Category, CategoryNameResource>();
            //CreateMap<Product, ProductResource>();
            CreateMap<Order, OrderResource>();
            CreateMap<OrderProducts, OrderProductsResource>();
            CreateMap<Product, ProductResource>().ForMember(src => src.UnitOfMeasurement,
                opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
        }
    }
}
