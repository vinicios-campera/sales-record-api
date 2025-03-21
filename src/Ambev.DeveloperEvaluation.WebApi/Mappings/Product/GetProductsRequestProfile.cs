﻿using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ListProducts;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Product
{
    public class GetProductsRequestProfile : Profile
    {
        public GetProductsRequestProfile()
        {
            CreateMap<GetProductsRequest, GetProductsCommand>();
            CreateMap<CommonPaginatedRequest, GetProductsCommand>();
        }
    }
}