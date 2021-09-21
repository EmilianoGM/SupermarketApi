using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using SupermarketApi.Entities;
using SupermarketApi.WebApi.Resources;

namespace SupermarketAPI.Mapping
{
    public class ResourceToModelProfile: Profile
    {
        public ResourceToModelProfile()
        {
            CreateMap<SaveCategoryResource, Category>();
            //CreateMap<SaveProductResource, Product>();
                
            CreateMap<SaveProductResource, Product>().ForMember(dest => dest.UnitOfMeasurement, 
                opt => opt.MapFrom(src => (EUnitOfMeasurement)src.UnitOfMeasurement));
            CreateMap<SaveOrderResource, Order>().ForMember(dest => dest.Date, 
                opt => opt.MapFrom(src => Convert.ToDateTime(src.Date)));

        }
    }
}
