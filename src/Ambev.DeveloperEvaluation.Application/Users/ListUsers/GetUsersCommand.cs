using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class GetUsersCommand : IRequest<GetUsersResult>
    {
        public int Page { get; set; }
        public int Size { get; set; }
        public string? Order { get; set; } = string.Empty;
        public string? Title { get; set; } = string.Empty;
    }
}