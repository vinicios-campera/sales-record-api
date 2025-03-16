using Ambev.DeveloperEvaluation.Application.Users.ListUsers;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.User
{
    public class GetUsersRequestProfile : Profile
    {
        public GetUsersRequestProfile()
        {
            CreateMap<CommonPaginatedRequest, GetUsersCommand>();
        }
    }
}