using AutoMapper;
using SupermarketAPI.Domain.Models;
using SupermarketAPI.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SupermarketAPI.Extensions;

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
            CreateMap<Category, CategoryResource>();
            CreateMap<Product, ProductResource>().ForMember(src => src.UnitOfMeasurement,
                opt => opt.MapFrom(src => src.UnitOfMeasurement.ToDescriptionString()));
        }
    }
}
