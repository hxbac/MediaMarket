﻿using MediaMarket.Application.DTO.User;
using MediaMarket.Domain.Entities;
using MediaMarket.Domain.Enums;
using Entities = MediaMarket.Domain.Entities;

namespace MediaMarket.Application.DTO.Response.Product
{
    public class ProductDetailResponse
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Slug { get; set; }
        public string? Thumbnail { get; set; }
        public string? ShortDescription { get; set; }
        public string? Description { get; set; }
        public long Price { get; set; }
        public ProductType ProductType { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public ProductContentStatus ProductContentStatus { get; set; }

        public Seller? Seller { get; set; }
        public ICollection<Entities.Tag>? Tags { get; set; }
        public ICollection<Entities.Category>? Categories { get; set; }
        public Entities.Preview? Preview { get; set; }
        public ICollection<ProductDiscount> Discounts { get; set; } = new List<ProductDiscount>();
        public List<Entities.EventDiscount> EventDiscounts { get; set; } = new List<Entities.EventDiscount>();
    }
}
