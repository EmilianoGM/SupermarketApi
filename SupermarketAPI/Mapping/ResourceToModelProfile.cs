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
                
            CreateMap<SaveProductResource, Product>().ForMember(p => p.UnitOfMeasurement, 
                opt => opt.MapFrom(p => (EUnitOfMeasurement)p.UnitOfMeasurement));
        }
    }
}
