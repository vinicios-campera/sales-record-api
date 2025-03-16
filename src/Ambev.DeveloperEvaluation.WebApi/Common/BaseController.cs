using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected string GetCurrentUserId() =>
            User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException();

    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

    protected IActionResult Okay<T>(T data) =>
            base.Ok(new ApiResponseWithData<T> { Data = data });

    //protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
    //    base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    //protected IActionResult BadRequest(string message) =>
    //    base.BadRequest(new ApiResponse { Detail = message, Success = false });

    //protected IActionResult NotFound(string message = "Resource not found") =>
    //    base.NotFound(new ApiResponse { Detail = message, Success = false });

    protected IActionResult OkayPaginated<T>(PaginatedList<T> pagedList) =>
            Ok(new PaginatedResponse<T>
            {
                Data = pagedList,
                CurrentPage = pagedList.CurrentPage,
                TotalPages = pagedList.TotalPages,
                TotalItems = pagedList.TotalItems,
            });

    protected string GetUrl(string resource)
    {
        var uriBuilder = new UriBuilder
        {
            Scheme = Request?.Scheme,
            Host = Request?.Host.Host,
            Path = resource,
            Port = Request?.Host.Port ?? -1
        };
        return uriBuilder.ToString();
    }
}