using Ambev.DeveloperEvaluation.Application.Users.GetUser;

namespace Ambev.DeveloperEvaluation.Application.Users.ListUsers
{
    public class GetUsersResult
    {
        public int TotalItems { get; set; }

        public IEnumerable<GetUserResult> Items { get; set; } = [];
    }
}