﻿using Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Product
{
    public class UpdateProductRequestProfile : Profile
    {
        public UpdateProductRequestProfile()
        {
            CreateMap<UpdateProductRequest, UpdateProductCommand>();
            CreateMap<UpdateProductRequest, UpdateProductResponse>();
        }
    }
}