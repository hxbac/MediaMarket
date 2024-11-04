﻿using AutoMapper;
using MediaMarket.Application.DTO.Response.Product;
using MediaMarket.Domain.Entities;

namespace MediaMarket.Application.Mappings
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductHomePageResponse>();
        }
    }
}