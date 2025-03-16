﻿using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.User
{
    public class UpdateUserRequestProfile : Profile
    {
        public UpdateUserRequestProfile()
        {
            CreateMap<UpdateUserRequest, UpdateUserCommand>();
            CreateMap<UpdateUserRequest, UpdateUserResponse>();
        }
    }
}