using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class GetUsersHandler(IUserRepository userRepository, IMapper mapper) : IRequestHandler<GetUsersCommand, GetUsersResult>
    {
        public async Task<GetUsersResult> Handle(GetUsersCommand request, CancellationToken cancellationToken)
        {
            var data = await userRepository.FilterAsync(request.Page, request.Size, request.Order, cancellationToken);
            var result = new GetUsersResult { TotalItems = data.Item2, Items = mapper.Map<IEnumerable<GetUserResult>>(data.Item1) };
            return result;
        }
    }
}